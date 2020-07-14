﻿using MediatR;

using MicroServices.Banking.Domain.Commands;
using MicroServices.Banking.Domain.Events;
using MicroServices.Domain.Core.Bus;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroServices.Banking.Domain.CommandHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IEventBus _eventBus;
        public TransferCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            //Publish Event to RabbitMQ Bus
            _eventBus.Publish(new TransferCreatedEvent(request.From,request.To,request.Amount));
            return Task.FromResult(true);
        }
    }
}
