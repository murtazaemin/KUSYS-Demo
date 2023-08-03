$(document).ready(function () {

    // Öğrenci listesinin çekilmesi
    var datatable = $('#StudentTable').DataTable({
        language: {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/tr.json"
        },
        "serverSide": true,
        "autoWidth": false,
        "ordering": true,
        "searching": true,
        "paging": true,
        ajax: {
            url: '/Student/GetStudentList',
            type: 'POST',
            dataType: "json"
        },
        columns: [
            {
                "data": "firstName", "title": "First Name"
            },
            {
                "data": "lastName", "title": "Last Name "
            },
            {
                "data": "birthDay", "title": "Birthday",
                "render": function (data) {
                    var date = new Date(data);
                    const options = {
                        dateStyle:'short',                      
                    };                   
                    return date.toLocaleString('tr-TR', options);
                }
            },
            {
                "data": "createDate", "name": "Create Date",
                "render": function (data) {
                    
                    var date = new Date(data);
                    const options = { 
                        dateStyle: 'short',                       
                    };
                    return date.toLocaleString('tr-TR', options);
                }
            },
            {
                'data': null,
                'render': function (data, type, row) {
                    return "<button class='btn btn-warning btnDuzenle' value='" + row.studentId + "'>Edit</button> <button class='btn btn-danger btnSil' value='" + row.studentId + "'>Delete</button>"
                }
            }
        ]
    });

    // Öğrencinin eklenmesi
    $("#AddStudent").click(function () {
        let student = {
            FirstName: $("#firstName").val(),
            LastName: $("#lastName").val(),
            BirthDate: $("#birthday").val()
        };

        $.ajax({
            type: "POST",
            url: "/Student/AddStudent",
            data: student,
            success: function (data) {

                if (data == "1") {
                    swal('Congratulations !', 'Data successfully added', 'success');
                    datatable.draw();

                    $("#add_student_form")[0].reset();
                }
                else {
                    swal('ERROR!', 'An error occurred while adding data', 'error');

                }

            },
            error: function () {
                swal('ERROR!', 'An error occurred while adding data', 'error');
            }
        })
    })

    // Formun sıfırlanması
    $("#CancelStudent").click(function () {
        $("#add_student_form")[0].reset();
    })

    // Düzenlenen öğrenci verilerinin set edilmesi
    $("#EditStudent").click(function () {

        let student = {
            StudentId: $("#studentId").val(),
            FirstName: $("#editFirstName").val(),
            LastName: $("#editLastName").val(),
            BirthDate: $("#editBirthday").val(),
        };

        $.ajax({
            type: "POST",
            url: "/Student/EditStudent",
            data: student,
            success: function (data) {

                if (data == "1") {
                    swal('Congratulations !', 'Data successfully edited', 'success');
                    datatable.draw();

                    $("#edit_food_form")[0].reset();
                }
                else {
                    swal('ERROR!', 'An error occurred while editing data', 'error');
                }
            },
            error: function () {
                swal('ERROR!', 'An error occurred while editing data', 'error');
            }
        })
    })

    // Seçili öğrecinin silinmesi
    $("#SetStudentList").on("click", ".btnSil", function () {

        if (confirm("Are you sure you want to delete?")) {
            let id = $(this).val();
            $.ajax({
                type: "POST",
                url: "/Student/DeleteStudent/",
                data: { "id": id },
                success: function (data) {
                    if (data == "1") {
                        swal('Congratulations !', 'Data successfully deleted', 'success');;
                        datatable.draw();
                    }
                    else {
                        swal('ERROR!', 'An error occurred while deleting data', 'error');
                    }
                },
                error: function () {
                    swal('ERROR!', 'An error occurred while deleting data', 'error');
                }
            })
        }

    })

    // Seçilen öğrencinin bilgilerinin çek forma set et
    $("#SetStudentList").on("click", ".btnDuzenle", function () {
        $('#ModalStudentEdit').modal('show');
        let id = $(this).val();
        const options = {
            dateStyle:"short"
        }

        $.ajax({
            type: "GET",
            url: "/Student/GetStudentById?id="+id,

            success: function (data) {
                $("#studentId").val(id);
                $("#editFirstName").val(data.firstName);
                $("#editLastName").val(data.lastName);
                let date = new Date(data.birthDate);
                $("#editBirthday").val(date.getFullYear() + "-" + String(date.getMonth() + 1).padStart(2, '0') + "-" + String(date.getDate()).padStart(2, '0'));
            },
            error: function () {
                swal('ERROR!', 'An error occurred while editing data', 'error');
            }
        })

    })
   
});