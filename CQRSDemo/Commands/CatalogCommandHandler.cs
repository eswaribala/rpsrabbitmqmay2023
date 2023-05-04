using CQRSDemo.Events;
using CQRSDemo.Models;
using CQRSDemo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public class CatalogCommandHandler : ICommandHandler<Command>
    {
        private CatalogSqliteRepository _repository;
        private AMQPEventPublisher _eventPublisher;
        public CatalogCommandHandler(AMQPEventPublisher eventPublisher, CatalogSqliteRepository repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }
        public void Execute(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command is null");
            }
            if (command is CreateCatalogCommand createCommand)
            {
                Catalog created = _repository.Create(createCommand.ToCatalogRecord());
                _eventPublisher.PublishEvent(createCommand.ToCartEvent(created.CatalogId));
            }
            else if (command is UpdateCatalogCommand updateCommand)
            {
                Catalog record = _repository.GetById(updateCommand.Id);
                _repository.Update(updateCommand.ToCartRecord(record));
                _eventPublisher.PublishEvent(updateCommand.ToCartEvent());
            }
            else if (command is DeleteCatalogCommand deleteCommand)
            {
                _repository.Remove(deleteCommand.Id);
                _eventPublisher.PublishEvent(deleteCommand.ToCartEvent());
            }
        }
    }
}
