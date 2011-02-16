using ISIS.Infrastructure;

namespace ISIS.Denormalizers.Tests
{
    public abstract class DenormalizerFixture<TDenormalizer, TEntity>
        : BaseFixture
        where TDenormalizer : IDenormalizer, new()
        where TEntity : Entity, new()
    {

        protected TDenormalizer Denormalizer { get; set; }
        protected TEntity Entity { get; set; }

        protected override void  OnTestSetup()
        {
            Denormalizer = new TDenormalizer();
            Denormalizer.Setup();
            Entity = new TEntity();
        }

        protected override void OnTestTearDown()
        {
            Denormalizer.TryTeardown();
        }

    }
}
