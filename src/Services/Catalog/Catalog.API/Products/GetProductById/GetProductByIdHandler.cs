﻿namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler(IDocumentSession documentSession)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await documentSession.LoadAsync<Product>(request.Id, cancellationToken);
            if(product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }
            return new GetProductByIdResult(product);
        }
    }
}