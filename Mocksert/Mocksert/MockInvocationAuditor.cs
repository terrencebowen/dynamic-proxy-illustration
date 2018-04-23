using Castle.DynamicProxy;
using KellermanSoftware.CompareNetObjects;
using Mocksert.Arrangements;
using Mocksert.Exceptions;
using Mocksert.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Mocksert
{
    internal interface IMockInvocationAuditor
    {
        long GetProxyIdentifier<TProxy>(TProxy proxy) where TProxy : class;
        void AddArrangement(IArrangement arrangement);
        void EnqueueInvocation(IInvocation invocation);
        void UpdateInvocationCount(IInvocation invocation);
        void Clear(long proxyIdentifier);
        IArrangement GetArrangement(IInvocation invocation);
        int GetArrangementCallsMade(long proxyIdentifier, int metadataToken);
        int GetInvocationCallsMade(long proxyIdentifier, int metadataToken);
        void VerifyGenericTypeArguments(IArrangement arrangement, IInvocation invocation);
        void VerifyArgumentValues(IArrangement arrangement, IInvocation invocation);
        void VerifyArrangementInvocationCounts(long proxyIdentifier);
        void DequeueInvocation(IInvocation invocation);
    }

    internal class MockInvocationAuditor : IMockInvocationAuditor
    {
        private readonly CompareLogic _compareLogic;
        private readonly IDictionary<long, IDictionary<int, int>> _arrangementCallsMade;
        private readonly IDictionary<long, IDictionary<int, int>> _invocationCallsMade;
        private readonly IDictionary<long, IDictionary<int, Queue<IArrangement>>> _arrangementQueue;
        private readonly IDictionary<long, IDictionary<int, Queue<IInvocation>>> _invocationQueue;
        private readonly IMockFailureMessageFactory _mockFailureMessageFactory;
        private readonly ObjectIDGenerator _objectIdGenerator;

        public MockInvocationAuditor()
        {
            _arrangementCallsMade = new Dictionary<long, IDictionary<int, int>>();
            _arrangementQueue = new Dictionary<long, IDictionary<int, Queue<IArrangement>>>();
            _compareLogic = new CompareLogic();
            _invocationCallsMade = new Dictionary<long, IDictionary<int, int>>();
            _invocationQueue = new Dictionary<long, IDictionary<int, Queue<IInvocation>>>();
            _mockFailureMessageFactory = new MockFailureMessageFactory();
            _objectIdGenerator = new ObjectIDGenerator();
        }

        public long GetProxyIdentifier<TProxy>(TProxy proxy) where TProxy : class
        {
            if (proxy == null)
            {
                var mockExceptionMessage = _mockFailureMessageFactory.GetNullProxyFailureMessage();

                throw new MockException(mockExceptionMessage);
            }

            var result = _objectIdGenerator.HasId(proxy, out var isObjectFirstAppearance);
            var isUnidentifiedObject = result == 0 || isObjectFirstAppearance;

            var proxyIdentifier = isUnidentifiedObject
                ? _objectIdGenerator.GetId(proxy, out isObjectFirstAppearance)
                : result;

            return proxyIdentifier;
        }

        public void AddArrangement(IArrangement arrangement)
        {
            var proxy = arrangement.Proxy;
            var proxyIdentifier = GetProxyIdentifier(proxy);
            var metadataToken = arrangement.MetadataToken;

            AddArrangement(arrangement, proxyIdentifier, metadataToken);
            UpdateArrangementCount(arrangement, proxyIdentifier, metadataToken);
        }

        public IArrangement GetArrangement(IInvocation invocation)
        {
            var proxy = invocation.Proxy;
            var proxyIdentifier = GetProxyIdentifier(proxy);
            var metadataToken = invocation.Method.MetadataToken;
            var memberName = invocation.GetFriendlyMemberName();
            var arrangementCount = GetArrangementCallsMade(proxyIdentifier, metadataToken);
            var invocationCount = GetInvocationCallsMade(proxyIdentifier, metadataToken);

            if (_arrangementQueue.ContainsKey(proxyIdentifier))
            {
                if (_arrangementQueue[proxyIdentifier].ContainsKey(metadataToken))
                {
                    var arrangementQueue = _arrangementQueue[proxyIdentifier][metadataToken];

                    if (arrangementQueue.TryDequeue(out var arrangement))
                    {
                        return arrangement;
                    }
                }
            }

            var mockExceptionMessage = _mockFailureMessageFactory.GetUnarrangedInvocationFailureMessage(memberName, arrangementCount, invocationCount);

            throw new MockException(mockExceptionMessage);
        }

        public void EnqueueInvocation(IInvocation invocation)
        {
            var proxy = invocation.Proxy;
            var proxyIdentifier = GetProxyIdentifier(proxy);
            var metadataToken = invocation.Method.MetadataToken;

            AddInvocation(invocation, proxyIdentifier, metadataToken);
        }

        public void DequeueInvocation(IInvocation invocation)
        {
            var proxy = invocation.Proxy;
            var proxyIdentifier = GetProxyIdentifier(proxy);
            var metadataToken = invocation.Method.MetadataToken;

            _invocationQueue[proxyIdentifier][metadataToken].Dequeue();
        }

        public void VerifyGenericTypeArguments(IArrangement arrangement, IInvocation invocation)
        {
            var arrangementMethod = arrangement as IArrangementMethod;
            var arrangementHasNoGenericTypeArguments = arrangementMethod == null;

            if (arrangementHasNoGenericTypeArguments)
            {
                return;
            }

            var arrangementGenericArguments = arrangementMethod.GenericArguments;
            var invocationGenericArguments = invocation.GenericArguments ?? new Type[0];
            var comparisonResult = _compareLogic.Compare(arrangementGenericArguments, invocationGenericArguments);

            if (comparisonResult.AreEqual)
            {
                return;
            }

            var mockExceptionMessage = _mockFailureMessageFactory.GetGenericTypeArgumentFailureMessage(arrangement, invocation);

            throw new MockException(mockExceptionMessage);
        }

        public void VerifyArgumentValues(IArrangement arrangement, IInvocation invocation)
        {
            object[] arrangementArguments;
            object[] invocationArguments;

            switch (arrangement)
            {
                case IArrangementProperty arrangementProperty:
                    var arguments = new[] { arrangementProperty.ReturnValue };

                    arrangementArguments = arguments;
                    invocationArguments = arguments;
                    break;

                case IArrangementMethod arrangementMethod:

                    arrangementArguments = arrangementMethod.ArrangementArguments
                                                            .Select(arrangementArgument => arrangementArgument.ArgumentValue)
                                                            .ToArray();

                    invocationArguments = invocation.Arguments;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(arrangement.GetType().Name);
            }

            var comparisonResult = _compareLogic.Compare(arrangementArguments, invocationArguments);

            if (comparisonResult.AreEqual)
            {
                return;
            }

            var mockExceptionMessage = _mockFailureMessageFactory.GetInvalidArgumentValueFailureMessage(arrangement, invocation);

            throw new MockException(mockExceptionMessage);
        }

        public void VerifyArrangementInvocationCounts(long proxyIdentifier)
        {
            foreach (var keyValuePair in _arrangementQueue[proxyIdentifier])
            {
                var queue = keyValuePair.Value;

                if (!queue.Any())
                {
                    continue;
                }

                var arrangement = queue.First();
                var metadataToken = arrangement.MetadataToken;
                var memberName = arrangement.GetFriendlyMemberName();
                var arrangementCount = GetArrangementCallsMade(proxyIdentifier, metadataToken);
                var invocationCount = GetInvocationCallsMade(proxyIdentifier, metadataToken);
                var mockExceptionMessage = _mockFailureMessageFactory.GetArrangementUninvokedFailureMessage(memberName, arrangementCount, invocationCount);

                throw new MockException(mockExceptionMessage);
            }

            foreach (var keyValuePair in _invocationQueue[proxyIdentifier])
            {
                var queue = keyValuePair.Value;

                if (!queue.Any())
                {
                    continue;
                }

                var invocation = queue.First();
                var metadataToken = invocation.Method.MetadataToken;
                var memberName = invocation.GetFriendlyMemberName();
                var arrangementCount = GetArrangementCallsMade(proxyIdentifier, metadataToken);
                var invocationCount = GetInvocationCallsMade(proxyIdentifier, metadataToken);
                var mockExceptionMessage = _mockFailureMessageFactory.GetUnarrangedInvocationFailureMessage(memberName, arrangementCount, invocationCount);

                throw new MockException(mockExceptionMessage);
            }
        }

        public int GetArrangementCallsMade(long proxyIdentifier, int metadataToken)
        {
            if (_arrangementCallsMade.ContainsKey(proxyIdentifier) &&
                _arrangementCallsMade[proxyIdentifier].ContainsKey(metadataToken))
            {
                var arrangementCallsMade = _arrangementCallsMade[proxyIdentifier][metadataToken];

                return arrangementCallsMade;
            }

            return 0;
        }

        public int GetInvocationCallsMade(long proxyIdentifier, int metadataToken)
        {
            if (_invocationCallsMade.ContainsKey(proxyIdentifier) &&
                _invocationCallsMade[proxyIdentifier].ContainsKey(metadataToken))
            {
                var invocationCallsMade = _invocationCallsMade[proxyIdentifier][metadataToken];

                return invocationCallsMade;
            }

            return 0;
        }

        public void UpdateInvocationCount(IInvocation invocation)
        {
            var proxy = invocation.Proxy;
            var proxyIdentifier = GetProxyIdentifier(proxy);
            var metadataToken = invocation.Method.MetadataToken;

            if (_invocationCallsMade.ContainsKey(proxyIdentifier))
            {
                if (_invocationCallsMade[proxyIdentifier].ContainsKey(metadataToken))
                {
                    var isPropertyArrangement = invocation.Method.IsSpecialName;

                    if (!isPropertyArrangement)
                    {
                        _invocationCallsMade[proxyIdentifier][metadataToken]++;
                    }
                }
                else
                {
                    _invocationCallsMade[proxyIdentifier].Add(metadataToken, 1);
                }
            }
            else
            {
                _invocationCallsMade.Add(proxyIdentifier, new Dictionary<int, int>
                {
                    { metadataToken, 1 }
                });
            }
        }

        public void Clear(long proxyIdentifier)
        {
            _arrangementCallsMade.Remove(proxyIdentifier);
            _arrangementQueue.Remove(proxyIdentifier);
            _invocationCallsMade.Remove(proxyIdentifier);
            _invocationQueue.Remove(proxyIdentifier);
        }

        private void AddArrangement(IArrangement arrangement, long proxyIdentifier, int metadataToken)
        {
            if (_arrangementQueue.ContainsKey(proxyIdentifier))
            {
                if (_arrangementQueue[proxyIdentifier].ContainsKey(metadataToken))
                {
                    _arrangementQueue[proxyIdentifier][metadataToken].Enqueue(arrangement);
                }
                else
                {
                    _arrangementQueue[proxyIdentifier].Add(metadataToken, new Queue<IArrangement>(new[] { arrangement }));
                }
            }
            else
            {
                _arrangementQueue.Add(proxyIdentifier, new Dictionary<int, Queue<IArrangement>>
                {
                    { metadataToken, new Queue<IArrangement>(new[] {arrangement}) }
                });
            }
        }

        private void AddInvocation(IInvocation invocation, long proxyIdentifier, int metadataToken)
        {
            if (_invocationQueue.ContainsKey(proxyIdentifier))
            {
                if (_invocationQueue[proxyIdentifier].ContainsKey(metadataToken))
                {
                    _invocationQueue[proxyIdentifier][metadataToken].Enqueue(invocation);
                }
                else
                {
                    _invocationQueue[proxyIdentifier].Add(metadataToken, new Queue<IInvocation>(new[] { invocation }));
                }
            }
            else
            {
                _invocationQueue.Add(proxyIdentifier, new Dictionary<int, Queue<IInvocation>>
                {
                    { metadataToken, new Queue<IInvocation>(new[] {invocation}) }
                });
            }
        }

        private void UpdateArrangementCount(IArrangement arrangement, long proxyIdentifier, int metadataToken)
        {
            if (_arrangementCallsMade.ContainsKey(proxyIdentifier))
            {
                if (_arrangementCallsMade[proxyIdentifier].ContainsKey(metadataToken))
                {
                    var isPropertyArrangement = arrangement is IArrangementProperty;

                    if (!isPropertyArrangement)
                    {
                        _arrangementCallsMade[proxyIdentifier][metadataToken]++;
                    }
                }
                else
                {
                    _arrangementCallsMade[proxyIdentifier].Add(metadataToken, 1);
                }
            }
            else
            {
                _arrangementCallsMade.Add(proxyIdentifier, new Dictionary<int, int>
                {
                    { metadataToken, 1 }
                });
            }
        }
    }
}