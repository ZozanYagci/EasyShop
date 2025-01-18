function updateProductList(products) {
    const productContainer = document.getElementById('product-list');
    productContainer.innerHTML = '';


    if (!products || !products.length) {
        productContainer.innerHTML = `<div class="col-12 text-center">
            <p>Şu anda görüntülenecek ürün bulunmamaktadır.</p>
        </div>`;
        return;
    }

    // Document Fragment ile DOM güncelleme
    const fragment = document.createDocumentFragment();

    products.forEach(product => {
        const currentPrice = product.prices.find(p => p.isCurrent) || {};
        const lastOldPrice = product.prices
            .filter(p => !p.isCurrent)
            .sort((a, b) => new Date(b.effectiveDate) - new Date(a.effectiveDate))[0];

        const productCard = document.createElement('div');
        productCard.className = 'col-lg-4 col-md-6 col-sm-6 pb-1';


        productCard.innerHTML = `
            <div class="product-item bg-light mb-4">
                <div class="product-img position-relative overflow-hidden">
                    <img class="img-fluid w-100" src="${product.imageUrl || '/images/placeholder.jpg'}" alt="">
                        <div class="product-action">
                            <a class="btn btn-outline-dark btn-square" href=""><i class="fa fa-shopping-cart"></i></a>
                            <a class="btn btn-outline-dark btn-square" href=""><i class="far fa-heart"></i></a>
                            <a class="btn btn-outline-dark btn-square" href=""><i class="fa fa-sync-alt"></i></a>
                            <a class="btn btn-outline-dark btn-square" href=""><i class="fa fa-search"></i></a>
                        </div>
                  </div>
                  <div class="text-center py-4">
                    <a class="h6 text-decoration-none text-truncate" href="">${product.name}</a>
                    <div class="d-flex align-items-center justify-content-center mt-2">
                        <h5>${currentPrice.price ? currentPrice.price.toLocaleString('tr-TR', { style: 'currency', currency: 'TRY' }) : ''}</h5>
                        ${lastOldPrice && lastOldPrice.price > currentPrice.price
                ? `<h6 class="text-muted ml-2"><del>${lastOldPrice.price.toLocaleString('tr-TR', { style: 'currency', currency: 'TRY' })}</del></h6>`
                : ''
            }
                   </div>
                   <div class="d-flex align-items-center justify-content-center mb-1">
                        <small class="fa fa-star text-primary mr-1"></small>
                        <small class="fa fa-star text-primary mr-1"></small>
                        <small class="fa fa-star text-primary mr-1"></small>
                        <small class="fa fa-star text-primary mr-1"></small>
                        <small class="fa fa-star text-primary mr-1"></small>
                        <small>(99)</small>
                    </div>
               </div>
            </div>`;
        fragment.appendChild(productCard);
    });
   
    productContainer.appendChild(fragment);
}