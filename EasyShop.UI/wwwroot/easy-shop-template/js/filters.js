document.addEventListener('DOMContentLoaded', () => {
    getAllProducts();
});

function getAllProducts() {
    fetch("https://localhost:44372/api/Products/ProductWithPrices", {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => response.json())
        .then(data => updateProductList(data))
        .catch(error => console.error("Ürünler getirilirken hata oluştu:", error));
}

document.querySelectorAll('.filter-checkbox').forEach(checkbox => {
    checkbox.addEventListener('change', () => {
        applyFilters();
    });
});

function applyFilters() {
    const selectedFilters = {
        colors: [],
        components: [],
        sizes: [],
        minPrice: null,
        maxPrice: null
    };
    let hasFilters = false;

    document.querySelectorAll('.filter-checkbox:checked').forEach(checkbox => {
        const filterType = checkbox.dataset.filterType;
        const value = checkbox.dataset.value;

        if (!selectedFilters[filterType]) {
            selectedFilters[filterType] = [];
        }
        if (filterType === "Renk") {
            selectedFilters.colors.push(value);
        }
        else if (filterType === "Materyal") {
            selectedFilters.components.push(value);
        }
        else if (filterType === "Beden") {
            selectedFilters.sizes.push(value);
        }
        else if (filterType === "Fiyat") {
            const [minPrice, maxPrice] = value.split('-').map(v => parseFloat(v));
            selectedFilters.minPrice = minPrice;
            selectedFilters.maxPrice = maxPrice;
        }
       
        hasFilters = true;
        
    });

    if (!hasFilters) {
        getAllProducts();
        return;
    }

    //if (Object.keys(selectedFilters).length === 0) {
    //    getAllProducts();
    //    return;
    //}

    console.log("Seçilen Filtreler:", selectedFilters); //kontrol

    const filterRequest = {
        colors: selectedFilters.colors.length > 0 ? selectedFilters.colors : [],
        components: selectedFilters.components.length > 0 ? selectedFilters.components : [],
        sizes: selectedFilters.sizes.length > 0 ? selectedFilters.sizes : [],
        minPrice: selectedFilters.minPrice ?? null,
        maxPrice: selectedFilters.maxPrice ?? null
    };


    //AJAX çağrısı
    fetch("https://localhost:44372/api/Filters/filtered-products", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(filterRequest)
    })
        .then(response => response.json())
        .then(data => updateProductList(data))
        .catch(error => console.error("Filtreleme hatası:", error));
}
