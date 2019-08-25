using NzbDrone.Core.Notifications;

namespace Lidarr.Api.V1.Notifications
{
    public class NotificationModule : ProviderModuleBase<NotificationResource, INotification, NotificationDefinition>
    {
        public static readonly NotificationResourceMapper ResourceMapper = new NotificationResourceMapper();

        public NotificationModule(NotificationFactory notificationFactory)
            : base(environment, notificationFactory, "notification", ResourceMapper)
        {
        }

        protected override void Validate(NotificationDefinition definition, bool includeWarnings)
        {
            if (!definition.Enable) return;
            base.Validate(definition, includeWarnings);
        }
    }
}
