document.addEventListener('DOMContentLoaded', async function () {
    // Sayfa y�klendi�inde okul listesini y�kleyelim
    try {
        const response = await fetch('/api/school');
        const schools = await response.json();

        // API yan�t�n� kontrol edelim
        console.log('Okul listesi:', schools);

        const schoolSelect = document.getElementById('schoolName'); // select ��esini al

        // Select elementinin do�ru olup olmad���n� kontrol edelim
        console.log('Select eleman�:', schoolSelect);

        schools.forEach(school => {
            const option = document.createElement('option');
            option.value = school;
            option.textContent = school;
            schoolSelect.appendChild(option);
        });
    } catch (error) {
        console.error('Okul listesi yuklenirken hata olustu:', error);
    }

    const btn = document.getElementById('tikla'); // ��e se�imi
    if (btn) {
        btn.addEventListener('click', async function (event) { // btn �zerine click olay�n� ekle
            event.preventDefault(); // Form g�nderimini durdur

            // Olay i�leyici
            if (document.getElementById('firstName').value.trim() === "") {
                document.getElementById('firstName').style.borderColor = "red";
                return;
            } else {
                document.getElementById('firstName').style.borderColor = "";
            }

            if (document.getElementById('lastName').value.trim() === "") {
                document.getElementById('lastName').style.borderColor = "red";
                return;
            } else {
                document.getElementById('lastName').style.borderColor = "";
            }

            if (document.getElementById('email').value.trim() === "") {
                document.getElementById('email').style.borderColor = "red";
                return;
            } else {
                document.getElementById('email').style.borderColor = "";
            }

            if (document.getElementById('phone').value.trim() === "") {
                document.getElementById('phone').style.borderColor = "red";
                return;
            } else {
                document.getElementById('phone').style.borderColor = "";
            }

            if (document.getElementById('birthDate').value.trim() === "") {
                document.getElementById('birthDate').style.borderColor = "red";
                return;
            } else {
                document.getElementById('birthDate').style.borderColor = "";
            }

            if (document.getElementById('internStartDate').value.trim() === "") {
                document.getElementById('internStartDate').style.borderColor = "red";
                return;
            } else {
                document.getElementById('internStartDate').style.borderColor = "";
            }

            if (document.getElementById('internEndDate').value.trim() === "") {
                document.getElementById('internEndDate').style.borderColor = "red";
                return;
            } else {
                document.getElementById('internEndDate').style.borderColor = "";
            }

            if (document.getElementById('schoolName').value.trim() === "") {
                document.getElementById('schoolName').style.borderColor = "red";
                return;
            } else {
                document.getElementById('schoolName').style.borderColor = "";
            }

            if (document.getElementById('academicMajor').value.trim() === "") {
                document.getElementById('academicMajor').style.borderColor = "red";
                return;
            } else {
                document.getElementById('academicMajor').style.borderColor = "";
            }

            // Form verilerini haz�rlayal�m
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
                const response = await fetch('/api/intern', { //InternController.cs in  intern k�sm�n� al�yor.
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(formData)
                });

                if (response.ok) {
                    alert('Eklenen Stajyer: ' + document.getElementById('firstName').value + ' ' + document.getElementById('lastName').value);

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
                    const errorText = await response.text();
                    alert('Eklerken hata olustu! ' + errorText);
                }
            } catch (error) {
                console.error('Error:', error);
                alert('Stajyer eklenirken hata olustu.');
            }
        });
    }
});