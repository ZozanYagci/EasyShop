let currentSort = "newest";

document.addEventListener('DOMContentLoaded', () => {
    getFilteredProducts();
});

document.querySelectorAll('.filter-checkbox').forEach(checkbox => {
    checkbox.addEventListener('change', () => {
        getFilteredProducts();
    });
});

document.querySelectorAll('.sort-link').forEach(link => {
    link.addEventListener('click', (e) => {
        e.preventDefault();
        currentSort = e.target.dataset.sort;
        console.log("Seçilen Sıralama:", currentSort);
        getFilteredProducts();
    });
});

function getFilteredProducts() {
    const selectedFilters = {
        colors: [],
        components: [],
        sizes: [],
        minPrice: null,
        maxPrice: null
    };

    document.querySelectorAll('.filter-checkbox:checked').forEach(checkbox => {
        const filterType = checkbox.dataset.filterType;
        const value = checkbox.dataset.value;

        if (filterType === "Renk") {
            selectedFilters.colors.push(value);
        } else if (filterType === "Materyal") {
            selectedFilters.components.push(value);
        } else if (filterType === "Beden") {
            selectedFilters.sizes.push(value);
        } else if (filterType === "Fiyat") {
            const [minPrice, maxPrice] = value.split('-').map(Number);
            selectedFilters.minPrice = minPrice;
            selectedFilters.maxPrice = maxPrice;
        }
    });

    const filterRequest = {
        colors: selectedFilters.colors,
        components: selectedFilters.components,
        sizes: selectedFilters.sizes,
        minPrice: selectedFilters.minPrice ?? null,
        maxPrice: selectedFilters.maxPrice ?? null,
        sortBy: currentSort
    };

    console.log("Gönderilen filtre + sıralama:", filterRequest);

    fetch("https://localhost:7090/api/Filters/filtered-products", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(filterRequest)
    })
        .then(response => response.json())
        .then(data => updateProductList(data))
        .catch(error => console.error("Filtreleme / sıralama hatası:", error));
}
