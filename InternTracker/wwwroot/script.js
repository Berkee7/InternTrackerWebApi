document.addEventListener('DOMContentLoaded', function () {
    const btn = document.getElementById('tikla'); // öðe seçimi
    if (btn) {
        btn.addEventListener('click', async function (event) { // btn üzerine click olayýný ekle
            event.preventDefault(); // Form gönderimini durdur

            // Olay iþleyici
            if (document.getElementById('firstName').value.trim() === "") {
                document.getElementById('firstName').style.borderColor = "red";
                return; // Fonksiyondan çýk
            } else {

                document.getElementById('firstName').style.borderColor = "";
            }

            if (document.getElementById('lastName').value.trim() === "") {
                document.getElementById('lastName').style.borderColor = "red";
                return; // Fonksiyondan çýk
            } else {

                document.getElementById('lastName').style.borderColor = "";
            }

            if (document.getElementById('email').value.trim() === "") {
                document.getElementById('email').style.borderColor = "red";
                return; // Fonksiyondan çýk
            } else {

                document.getElementById('email').style.borderColor = "";
            }

            if (document.getElementById('phone').value.trim() === "") {
                document.getElementById('phone').style.borderColor = "red";
                return; // Fonksiyondan çýk
            } else {

                document.getElementById('phone').style.borderColor = "";
            }

            if (document.getElementById('birthDate').value.trim() === "") {
                document.getElementById('birthDate').style.borderColor = "red";
                return; // Fonksiyondan çýk
            } else {

                document.getElementById('birthDate').style.borderColor = "";
            }

            if (document.getElementById('internStartDate').value.trim() === "") {
                document.getElementById('internStartDate').style.borderColor = "red";
                return; // Fonksiyondan çýk
            } else {

                document.getElementById('internStartDate').style.borderColor = "";
            }
            if (document.getElementById('internEndDate').value.trim() === "") {
                document.getElementById('internEndDate').style.borderColor = "red";
                return; // Fonksiyondan çýk
            } else {

                document.getElementById('internEndDate').style.borderColor = "";
            }



            if (document.getElementById('schoolName').value.trim() === "") {
                document.getElementById('schoolName').style.borderColor = "red";
                return; // Fonksiyondan çýk
            } else {

                document.getElementById('schoolName').style.borderColor = "";
            }
            if (document.getElementById('academicMajor').value.trim() === "") {
                document.getElementById('academicMajor').style.borderColor = "red";
                return; // Fonksiyondan çýk
            } else {

                document.getElementById('academicMajor').style.borderColor = "";
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
                    alert('Baþarýlý bir þekilde eklendi!');

                    // Form alanlarýný temizle
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
                    const errorText = await response.text(); // Asenkron olarak yanýtý al
                    alert('Eklerken hata oluþtu! ' + errorText);
                }
            } catch (error) {
                console.error('Error:', error);
                alert('Stajyer eklenirken hata oluþtu.');
            }
        });
    }
});
