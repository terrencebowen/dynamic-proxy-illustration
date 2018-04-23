namespace Mocksert.Arrangements
{
    internal interface IArrangementArgument
    {
        string ArgumentName { get; }
        string FriendlyTypeName { get; }
        string OrdinalPosition { get; }
        object ArgumentValue { get; }
    }

    internal class ArrangementArgument : IArrangementArgument
    {
        public string ArgumentName { get; }
        public string FriendlyTypeName { get; }
        public string OrdinalPosition { get; }
        public object ArgumentValue { get; }

        public ArrangementArgument(string argumentName, string friendlyTypeName, string ordinalPosition, object argumentValue)
        {
            ArgumentName = argumentName;
            ArgumentValue = argumentValue;
            FriendlyTypeName = friendlyTypeName;
            OrdinalPosition = ordinalPosition;
        }
    }
}