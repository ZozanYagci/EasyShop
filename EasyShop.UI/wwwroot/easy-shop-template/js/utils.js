async function sendRequest(url, method, body) {
    try {
        const response = await fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(body)
        });

        const data = await response.json();

        if (!response.ok) {
            return data;
        }

        return data;

    } catch (error) {
        console.error('Hata:', error);
        return ("Bağlantı hatası! Lütfen tekrar deneyin.", "danger");
        /*   return null;*/
    }
}

function showToast(message, type = "info") {
    console.log("Message:", message, "Type:", type);
    const toastContainer = document.getElementById("toastContainer");
    if (!toastContainer) {
        console.error("Toast container bulunamadı.");
        return;
    }

    toastContainer.innerHTML = '';

    const toast = document.createElement("div");
    toast.className = `toast align-items-center text-white bg-${type} border-0 show`;
    toast.setAttribute("role", "alert");
    toast.setAttribute("aria-live", "assertive");
    toast.setAttribute("aria-atomic", "true");

    //toast.innerHTML = `
    //    <div class="d-flex">
    //        <div class="toast-body">
    //            ${message}
    //        </div>
    //        <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
    //    </div>
    //`;
    const toastBody = document.createElement("div");
    toastBody.className = "toast-body";
    toastBody.textContent = message.replace(/\n/g, "<br>");

    const closeButton = document.createElement("button");
    closeButton.type = "button";
    closeButton.className = "btn-close btn-close-white me-2 m-auto";
    closeButton.setAttribute("data-bs-dismiss", "toast");
    closeButton.setAttribute("aria-label", "Close");


    toast.appendChild(toastBody);
    toast.appendChild(closeButton);
    toastContainer.appendChild(toast);

    setTimeout(() => {
        toast.classList.remove("show");
        setTimeout(() => toast.remove(), 500);
    }, 3000);
}
