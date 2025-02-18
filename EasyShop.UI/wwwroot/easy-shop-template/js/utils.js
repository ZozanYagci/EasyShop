async function sendRequest(url, method, body) {
    try {
        const response = await fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(body)
        });

        if (!response.ok) {
            throw new Error("Sunucu hatası! Lütfen tekrar deneyin.");
        }

        return await response.json();
    } catch (error) {
        console.error('Hata:', error);
        showToast("Bağlantı hatası! Lütfen tekrar deneyin.", "danger");
        return null;
    }
}

function showToast(message, type = "info") {
    console.log("Message:", message, "Type:", type);
    const toastContainer = document.getElementById("toastContainer");
    if (!toastContainer) {
        console.error("Toast container bulunamadı.");
        return;
    }

    const toast = document.createElement("div");
    toast.className = `toast align-items-center text-white bg-${type} border-0 show`;
    toast.setAttribute("role", "alert");
    toast.setAttribute("aria-live", "assertive");
    toast.setAttribute("aria-atomic", "true");

    toast.innerHTML = `
        <div class="d-flex">
            <div class="toast-body">
                ${message}
            </div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
    `;

    toastContainer.appendChild(toast);

    setTimeout(() => {
        toast.classList.remove("show");
        setTimeout(() => toast.remove(), 500);
    }, 3000);
}
