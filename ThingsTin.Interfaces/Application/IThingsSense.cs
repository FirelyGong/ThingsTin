using ThingsTin.Interfaces.Container;

namespace ThingsTin.Interfaces.Application
{
    public interface IThingsSense
    {
        void Initialize(IThingsTin frame);

        IThingsFunction Functions { get; set; }

        IAbout About { get; set; }
    }
}
