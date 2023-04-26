using Lillogrenda.Charging.Domain.Entities;
using MediatR;

namespace Lillogrenda.Charging.Application.Invoices.Queries;

public class GetInvoicesQuery : IRequest<IEnumerable<InvoiceLine>>
{
    
}