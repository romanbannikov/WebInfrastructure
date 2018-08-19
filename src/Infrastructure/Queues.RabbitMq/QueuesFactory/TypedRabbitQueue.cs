﻿namespace Skeleton.Queues.RabbitMq.QueuesFactory
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions;
    using Abstractions.QueuesFactory;
    using Abstractions.QueuesFactory.ExceptionsHandling;
    using Abstractions.QueuesFactory.ExceptionsHandling.Handlers;
    using Microsoft.Extensions.Logging;
    using RabbitMQ.Client;

    public class TypedRabbitQueue<TMessage> : RabbitQueue, ITypedQueue<TMessage>
    {
        public TypedRabbitQueue(
            string name, 
            IConnection connection, 
            int retriesCount, 
            TimeSpan retryInitialTimeout,
            ITypedQueue<ExceptionDescription> errorsQueue,
            ExceptionHandlerBase<RabbitMessageDescription> exceptionHandler,
            ILogger<TypedRabbitQueue<TMessage>> logger) 
            : base(name, connection, retriesCount, retryInitialTimeout, errorsQueue, exceptionHandler, logger)
        {
        }

        public async Task<ITypedQueue<TMessage>> SendMessageAsync(
            TMessage message, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.SendMessageAsync(message, cancellationToken);
            return this;
        }

        public async Task<ITypedQueue<TMessage>> SubscribeAsync(
            IMessageHandler<TMessage> handler, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.SubscribeAsync(handler, cancellationToken);
            return this;
        }
    }
}