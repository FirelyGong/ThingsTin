
namespace ThingsTin.Interfaces.Application
{
    public abstract class AbstractThing:IThingsSense
    {
        public virtual void Initialize(Container.IThingsTin frame)
        {
        }

        public virtual IThingsFunction Functions
        {
            get;
            set;
        }

        public virtual IAbout About
        {
            get;
            set;
        }
    }
}
