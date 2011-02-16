namespace ISIS
{
    public abstract class Entity : DynamicModel
    {

        public const string ConnectionStringName = "ReadModel";

        protected Entity()
            : base(ConnectionStringName)
        {
        }

    }
}
