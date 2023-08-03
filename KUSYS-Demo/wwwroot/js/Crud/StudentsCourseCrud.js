$(document).ready(function () {

    // Selectbox için öğrenci listesi
    $.ajax('/Student/GetStudentListForDropdown', {
        type: 'GET',
        dataType: "json",
        success: function (data, status, xhr) {
            $('#studentName').append("<option value='0'>Choose Student...</option>");
            $.each(data.data, function (index, row) {
                $('#studentName').append("<option value='" + row.studentId + "'>" + row.studentName + "</option>")
            });

        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('#studentName').append('Error' + errorMessage);
        }
    });

    // Selectbox için kurs listesi
    $.ajax('/Course/GetCourseListForDropdown', {
        type: 'GET',
        dataType: "json",
        success: function (data, status, xhr) {
            $('#courseName').append("<option value='0'>Choose Course...</option>");
            $.each(data.data, function (index, row) {
                $('#courseName').append("<option value='" + row.courseId + "'>" + row.courseName + "</option>")
            });
            
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $('#courseName').append('Error' + errorMessage);
        }
    });

    // Öğrenci Kurs Eşleşmeleri
    var datatable = $('#StudentsCourseTable').DataTable({
        language: {
            "url": "https://cdn.datatables.net/plug-ins/1.11.5/i18n/en.json"
        },
        "serverSide": true,
        "autoWidth": false,
        "ordering": true,
        "searching": true,
        "paging": true,
        ajax: {
            url: '/StudentsCourse/GetStudentsCourseList',
            type: 'POST',
            dataType: "json"
        },
        columns: [
            {
                "data": "studentFullName", "title": "Student Name"
            },
            {
                "data": "courseName", "title": "Course Name"
            },
            {
                "data": "courseCode", "title": "Course Code"
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
            }
        ]
    });

    // Öğrenci Kurs Seçimi
    $("#AddStudentsCourse").click(function () {
        let studentsCourse = {
            CourseId: $("#courseName").val(),
            StudentId: $("#studentName").val(),
        };

        $.ajax({
            type: "POST",
            url: "/StudentsCourse/AddStudentsCourse",
            data: studentsCourse,
            success: function (data) {

                if (data == "1") {
                    swal('Congratulations !', 'Data successfully added', 'success');
                    datatable.draw();

                    $("#add_students_course_form")[0].reset();
                }
                else if (data == "2") {                   
                    swal('ERROR!', 'This course has already been selected', 'error');                }
                else {
                    swal('ERROR!', 'An error occurred while adding data', 'error');
                    
                }

            },
            error: function () {
                swal('ERROR!', 'An error occurred while adding data', 'error');
            }
        })
    })
 
});