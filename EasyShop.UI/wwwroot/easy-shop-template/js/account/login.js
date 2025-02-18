document.addEventListener("DOMContentLoaded", function () {
    const loginForm = document.getElementById("loginForm");
    if (!loginForm) return;

    loginForm.addEventListener("submit", async function (event) {
        event.preventDefault();

        const email = document.getElementById("email")?.value.trim();
        const password = document.getElementById("password")?.value.trim();

        if (!email || !password) {
            showToast("Lütfen e-posta ve şifre alanlarını doldurun.", "warning");
            return;
        }

        const loginData = { email, password };
        const data = await sendRequest('https://localhost:44372/api/Auth/login', 'POST', loginData);

        if (data && data.token) { 
            showToast("Giriş başarılı! Hoş geldiniz.", "success");
            // Ana sayfaya 3 sn sonra yönlendir 
            setTimeout(() => {
                window.location.replace('/Default/Index');
            }, 3000);
        } else {
            showToast("Giriş başarısız! Lütfen tekrar deneyin.", "danger");
        }
    });
});

