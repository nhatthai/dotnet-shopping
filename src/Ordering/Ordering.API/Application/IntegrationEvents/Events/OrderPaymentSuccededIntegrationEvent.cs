﻿namespace Ordering.API.Application.IntegrationEvents.Events
{
    using BuildingBlocks.EventBus.Events;

    public class OrderPaymentSuccededIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderPaymentSuccededIntegrationEvent(int orderId) => OrderId = orderId;
    }
}