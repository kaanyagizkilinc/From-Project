
var YuklenenMedyaKayit = [];
var YuklenenOkulMedyaKayit = [];

document.addEventListener('DOMContentLoaded', function () {

    let counter = 1;
    document.getElementById('ekleBtn1').addEventListener('click', function (e) {
        e.preventDefault();
        counter++;
        const yeniDiv = document.createElement('div');
        yeniDiv.classList.add("media-div", "mt-3", "bg-dark", "rounded", "p-2");
        yeniDiv.setAttribute("data-id", counter);
        yeniDiv.innerHTML = `
                    <h5 class="text-light">Mezuniyet Bilgisi #${counter}</h5>
                <label class="form-label text-light"><strong>Okul Türü:</strong></label>
                <select class="form-control okul-select" id="Okul${counter}">
                    <option value="1">Lise</option>
                    <option value="2">Üniversite</option>
                </select>
                <label class="form-label text-light mt-2"><strong>Mezuniyet Tarihi:</strong></label>
                <input type="date" id="MezuniyetYili${counter}" class="form-control mezuniyet-tarihi">
                <label class="form-label text-light mt-2"><strong>Okul Adı:</strong></label>
                <input type="text" class="form-control okul-adi" id="OkulAdi${counter}" placeholder="Okul Adı giriniz...">
                <label class="form-label text-light mt-2"><strong>Diploma:</strong></label>
                <input type="file" class="form-control media-input" id="mediaIn${counter}" multiple>
                <img src="#" id="preview${counter}" alt="Preview img" style="width:200px; height: 200px; visibility:hidden; margin-top: 10px; border: 12px solid black; border-radius: 43px;" /></br>

                                <div id="diplomaPreview${counter}" class="d-flex flex-wrap mt-2"></div>
                                <button type="button" class="btn btn-danger mt-2 sil-btn">-</button>
                                <button type="button" class="btn btn-success mt-2 ekle-btn">+</button>
                

                `;
        document.getElementById('medyaAlanlari').appendChild(yeniDiv);
        const mediaInput = yeniDiv.querySelector('.media-input');
        const previewImg = yeniDiv.querySelector('img');


        
        mediaInput.addEventListener('change', function () {
            const file = this.files[0];
            if (file) {
                previewImg.src = URL.createObjectURL(file);
                previewImg.style.visibility = "visible";
            }
        });
    });

    document.getElementById('medyaAlanlari').addEventListener('click', function (e) {
        if (e.target.classList.contains('sil-btn')) {
            e.preventDefault();
            e.target.closest('.media-div').remove();
            yenidenNumarala();
        }
    });

    document.getElementById('medyaAlanlari').addEventListener('click', function (e) {
        if (e.target.classList.contains('ekle-btn')) {
            e.preventDefault();
            counter++;
            const yeniDiv = document.createElement('div');
            yeniDiv.classList.add("media-div", "mt-3", "bg-dark", "rounded", "p-2");
            yeniDiv.setAttribute("data-id", counter);
            yeniDiv.innerHTML = `
                    <h5 class="text-light">Mezuniyet Bilgisi #${counter}</h5>
                                <h5 class="text-light">Mezuniyet Bilgisi #${counter}</h5>
                                <input type="hidden" class="okul-id-hidden" value="${counter}" />

                                <label class="form-label text-light"><strong>Okul Türü:</strong></label>
                                <select class="form-control okul-select" id="Okul${counter}">
                                    <option value="1">Lise</option>
                                    <option value="2">Üniversite</option>
                                </select>

                                <label class="form-label text-light mt-2"><strong>Mezuniyet Tarihi:</strong></label>
                                <input type="date" id="MezuniyetYili${counter}" class="form-control mezuniyet-tarihi">

                                <label class="form-label text-light mt-2"><strong>Okul Adı:</strong></label>
                                <input type="text" class="form-control okul-adi" id="OkulAdi${counter}" placeholder="Okul Adı giriniz...">

                                <label class="form-label text-light mt-2"><strong>Diploma:</strong></label>
                                <input type="file" class="form-control media-input" id="mediaIn${counter}" name="Diploma" multiple>

                                <div id="diplomaPreview${counter}" class="d-flex flex-wrap mt-2"></div>
                                <button type="button" class="btn btn-danger mt-2 sil-btn">-</button>
                                <button type="button" class="btn btn-success mt-2 ekle-btn">+</button>
                

                `;
            document.getElementById('medyaAlanlari').appendChild(yeniDiv);
        }
    });

    function yenidenNumarala() {
        const box = document.querySelectorAll('.media-div');
        box.forEach((item, index) => {
            item.setAttribute('data-id', index + 1);
            const h5 = item.querySelector('h5');
            if (h5) h5.textContent = `Mezuniyet Bilgisi #${index + 1}`;
        });
        counter = box.length;
    }

        document.getElementById('mediaInput').addEventListener('change', function (event) {
            YuklenenMedyaKayit = Array.from(event.target.files);
            console.log("Yüklenen medya dosyaları:", YuklenenMedyaKayit);
        });
        document.getElementById("kaydetBtn").addEventListener("click", Kaydet);
    function Kaydet() {
        if (YuklenenMedyaKayit.length === 0) {
            Swal.fire({
                icon: 'warning',
                title: 'Uyarı!',
                text: 'Lütfen en az bir medya dosyası yükleyin.',
                confirmButtonText: 'Tamam'
            });
            return;
        }
        var cins;
        var erkek = document.getElementById('erkekcheck').value;
        var kadin = document.getElementById('kadincheck').value;
        if (erkek == "1") {
            cins = "1";
        }
        if (kadin == "1") {
            cins = "0";
        }

        var ad = $("#Ad").val();
        var soyad = $("#Soyad").val();
        var date = $("#DogumTarihi").val();
        if (!date) {
            alert("Doğum tarihi boş olamaz!");
            return;
        }
        var ulke = $("#Ulke").val();
        var sehir = $("#Sehir").val();
        var aciklama = $("#Aciklama").val();

        var model = {
            Ad: ad,
            Soyad: soyad,
            DogumTarihi: date,
            Cinsiyet: cins,
            Ulke: ulke,
            Sehir: sehir,
            SilindiMi: 0,
            Aciklama: aciklama
        };

        $.ajax({
            url: "/Home/KullaniciKayit",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(model),
            success: function (data) {
                var FormMediaData = new FormData();
                YuklenenMedyaKayit.forEach((file, index) => {
                    FormMediaData.append(`mediaFiles`, file);
                });
                FormMediaData.append('Id', data.id);
                $.ajax({
                    url: '/Home/MediaKayit',
                    data: FormMediaData,
                    type: 'POST',
                    contentType: false,
                    processData: false,
                    success: function (mediaResponse) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Kayıt Başarılı!',
                            text: 'Bilgiler başarıyla kaydedildi.',
                            confirmButtonText: 'Tamam'
                        });
                        console.log("Medya kaydı başarılı:", mediaResponse);


                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Medya Yükleme Hatası!',
                            text: 'Medya yüklenirken bir sorun oluştu.'
                        });
                    }
                });
                for (let i = 1; i <= counter; i++) {
                    const okulTuruEl = document.getElementById(`Okul${i}`).value;
                    const mezuniyetTarihiEl = document.getElementById(`MezuniyetYili${i}`).value;
                    const okulAdiEl = document.getElementById(`OkulAdi${i}`).value;
                    const diplomaEl = document.getElementById(`mediaIn${i}`);

                    if (!okulTuruEl || !mezuniyetTarihiEl || !okulAdiEl) continue; 

                    const okulTuru = okulTuruEl;
                    const mezuniyetTarihi = mezuniyetTarihiEl;
                    const okulAdi = okulAdiEl;

                    const KullanıcıOkuljson = {
                        KullaniciId: data.id,
                        OkulTuru: parseInt(okulTuru),
                        OkulAdi: okulAdi,
                        MezuniyetYili: mezuniyetTarihi,
                    };

                    console.log("Okul Model:", KullanıcıOkuljson);
                    $.ajax({
                        url: "/Home/MezuniyetKayit",
                        type: "POST",
                        contentType: "application/json",
                        data: JSON.stringify(KullanıcıOkuljson),
                        success: function (okulData) {
                            var OkulId = okulData.id; 
                            console.log("Mezuniyet kaydı başarılı:", okulData);
                            const kullaniciId = okulData.kullaniciId;
                            const formData = new FormData();
                            const diplomaFiles = diplomaEl.files;

                            if (!OkulId) {
                                console.log("OkulId null aga :" + OkulId);
                                return;
                            }
                            for (let i = 0; i < diplomaFiles.length; i++) {
                                formData.append('Diploma', diplomaFiles[i]);
                            }
                            formData.append('kullaniciId', kullaniciId);
                            formData.append('OkulId', OkulId);
                            console.log("Diploma File:", diplomaFiles);
                            if (diplomaFiles) {
                                $.ajax({
                                    url: '/Home/DiplomaKayit',
                                    type: 'POST',
                                    data: formData,
                                    contentType: false,
                                    processData: false,
                                    success: function (diplomaResponse) {
                                        console.log("Diploma kaydı başarılı:", diplomaResponse);
                                        setTimeout(() => window.location.href = "/Home/iletisimList", 2000);
                                    },
                                    error: function () {
                                        Swal.fire({
                                            icon: 'error',
                                            title: 'Diploma Yükleme Hatası!',
                                            text: 'Diploma yüklenirken bir sorun oluştu.'
                                        });
                                    }
                                });
                            }
                        
                        },
                        error: function () {
                            Swal.fire({
                                icon: 'error',
                                title: 'Okul Yükleme Hatası!',
                                text: 'Okul yüklenirken bir sorun oluştu.'
                            });
                        }
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Kullanıcı Kaydı Hatası!',
                    text: 'Kullanıcı kaydı sırasında bir sorun oluştu.'
                });
            }
        });
    }
});
