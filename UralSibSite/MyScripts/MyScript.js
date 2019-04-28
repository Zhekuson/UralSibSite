$(document).ready(function () {
    $(".element").click(function () {
        window.location.href = "~/Views/Home/DepartmentsInfo?Id=${this.id}";
        
    });
});
