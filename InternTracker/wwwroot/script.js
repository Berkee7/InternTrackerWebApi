document.addEventListener('DOMContentLoaded', function () {
    const btn = document.getElementById('tikla'); // ��e se�imi
    if (btn) {
        btn.addEventListener('click', async function (event) { // btn �zerine click olay�n� ekle
            event.preventDefault(); // Form g�nderimini durdur

            // Olay i�leyici
            if (document.getElementById('firstName').value.trim() === "") {
                document.getElementById('firstName').style.borderColor = "red";
                return; // Fonksiyondan ��k
            }

            if (document.getElementById('lastName').value.trim() === "") {
                document.getElementById('lastName').style.borderColor = "red";
                return; // Fonksiyondan ��k
            }
            if (document.getElementById('email').value.trim() === "") {
                document.getElementById('email').style.borderColor = "red";
                return; // Fonksiyondan ��k
            }

            if (document.getElementById('phone').value.trim() === "") {
                document.getElementById('phone').style.borderColor = "red";
                return; // Fonksiyondan ��k
            }
            if (document.getElementById('birthDate').value.trim() === "gg.aa.yyyy") {
                document.getElementById('birthDate').style.borderColor = "red";
                return; // Fonksiyondan ��k
            }

            if (document.getElementById('internStartDate').value.trim() === "gg.aa.yyyy") {
                document.getElementById('internStartDate').style.borderColor = "red";
                return; // Fonksiyondan ��k
            }
            if (document.getElementById('internEndDate').value.trim() === "gg.aa.yyyy") {
                document.getElementById('internEndDate').style.borderColor = "red";
                return; // Fonksiyondan ��k
            }

            const formData = {
                FirstName: document.getElementById('firstName').value,
                LastName: document.getElementById('lastName').value,
                Email: document.getElementById('email').value,
                Phone: document.getElementById('phone').value,
                BirthDate: document.getElementById('birthDate').value,
                InternStartDate: document.getElementById('internStartDate').value,
                InternEndDate: document.getElementById('internEndDate').value,
                SchoolTypeStr: parseInt(document.getElementById('schoolType').value),
                SchoolName: document.getElementById('schoolName').value,
                AcademicMajor: document.getElementById('academicMajor').value
            };

            try {
                const response = await fetch('/api/interns', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(formData)
                });

                if (response.ok) {
                    alert('Ba�ar�l� bir �ekilde eklendi!');

                    // Form alanlar�n� temizle
                    document.getElementById('firstName').value = "";
                    document.getElementById('lastName').value = "";
                    document.getElementById('email').value = "";
                    document.getElementById('phone').value = "";
                    document.getElementById('birthDate').value = "";
                    document.getElementById('internStartDate').value = "";
                    document.getElementById('internEndDate').value = "";
                    document.getElementById('schoolName').value = "";
                    document.getElementById('academicMajor').value = "";

                } else {
                    const errorText = await response.text(); // Asenkron olarak yan�t� al
                    alert('Eklerken hata olu�tu! ' + errorText);
                }
            } catch (error) {
                console.error('Error:', error);
                alert('Stajyer eklenirken hata olu�tu.');
            }
        });
    }
});
