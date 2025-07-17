document.addEventListener('DOMContentLoaded', function () {

    $("#registerForm").on("submit", function (e) {
        e.preventDefault();
        Register();
    });

    $("#loginForm").on("submit", function (e) {
        e.preventDefault();
        Login();
    });


    function Login() {
        $.ajax({
            url: '/Home/LoginKullanici',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                KullaniciAdi: $("#KullaniciAdi").val(),
                KullaniciSifre: $("#KullaniciSifre").val(),
                Salt: " "
            }),
            success: function (res) {
                let regex = /^(?=.*?[AZ])(?=.*?[az])(?=.*?[0-9]).{8,}/;
                var pass = $("#KullaniciSifre").val();
                if (!regex.test(pass)) {
                    Swal.fire({
                        icon: 'info',
                        title: 'Dikkat!',
                        text: "Şifre en az 8 karakter, bir büyük harf ve bir sayı içermelidir.",
                        confirmButtonText: 'Tamam'
                    });
                }
                console.log(res);
                $("#kullaniciBilgisi").text("Hoşgeldin " + res.kullaniciAdi);
                    Swal.fire({
                        icon: 'success',
                        title: 'Hoşgeldiniz ' + $("#KullaniciAdi").val(),
                        text: 'Giriş işlemi Gerçekleşti.',
                        confirmButtonText: 'Tamam'
                    });
                    setTimeout(() => window.location.href = "/Home/Index", 2000);/* Gelen kUllaniciAdi*/
            },
            error: function (xhr) {
                Swal.fire({
                    icon: 'error',
                    title: 'Hata!',
                    text: 'Lütfen kullanıcı adı ve Şifreyi gözden geçirin',
                    confirmButtonText: 'Tamam'
                });
            }
        });
    }

    function Register() {
        let regex = /^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{8,}$/;
        var pass = $("#KullaniciSifre").val();
        var passAgain = $("#KullaniciSifreTekrari").val()
        console.log(regex.test(pass));
        if (!regex.test(pass) && !regex.test(passAgain)) {
            Swal.fire({
                icon: 'info',
                title: 'Dikkat!',
                text: "Şifre en az 8 karakter, bir büyük harf ve bir sayı içermelidir.",
                confirmButtonText: 'Tamam'
            });
            return null;
        }
        if (pass == passAgain ) {
            $.ajax({
                url: '/Home/RegisterKullanici',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify({
                    KullaniciAdi: $("#KullaniciAdi").val(),
                    KullaniciSifre: $("#KullaniciSifre").val(),
                    Salt: " "
                }),
                success: function (model) {
                    console.log(model);
                    Swal.fire({
                        icon: 'success',
                        title: 'Kayıt Başarılı!',
                        text: 'Bilgiler başarıyla kaydedildi.',
                        confirmButtonText: 'Tamam'
                    });
                    setTimeout(() => window.location.href = "/Home/Login", 2000);
                },
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: 'Kayıt işlemi Gerçekleşmedi.',
                        confirmButtonText: 'Tamam'
                    });
                }
            });
        }
    }
});