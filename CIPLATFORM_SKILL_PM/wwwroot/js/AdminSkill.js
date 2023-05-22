$(document).ready(function () {
    getSkillData(1);
});
function getSkillData(pageNumber) {
    console.log("click");
    $.ajax({
        type: "Get",
        url: '/Admin/GetSkillData',
        data: { page: pageNumber},
        dataType: "html",
        success: function (data) {
            $("#skillTable").html("");
            $("#skillTable").html(data);
            editskill();
            addskill();
            deleteskill();
            toastr.success("dsaf");
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    });
}
var skillname = "";
var status = "";
function addskill() {
    $("#addskill").on("click", function () {
        skillname = $("#skillname").val();
        console.log(skillname);
        status = $("#status").val();
        console.log(status);
        $.ajax({
            type: "Post",
            url: '/Admin/AdminSkillAdd',
            data: { skillname: skillname, status: status },
            success: function (data) {
                alert("skill added successfully");
                location.reload();
            },
            error: function (xhr, status, error) {
                // Handle error
                alert("error");
            }

        });
    });
}
var skillId = "";
function editskill() {
    $(".editbuttonskill").on("click", function () {
        skillId = this.id
        $.ajax({
            type: "get",
            url: '/Admin/getSkillforedit',
            data: { id: skillId },
            success: function (data) {

                $("#editskillname").val(data.skillName);
                $("#editstatus").val("" + data.status);
                $("#EditSkillId").val("" + data.skillId);
            },
            error: function (xhr, status, error) {
                // Handle error
                alert("error");
            }

        });
    });
    var skillnameedit = "";
    var skillstatus = "";
    $("#EditSkill").on("click", function () {
        skillnameedit = $("#editskillname").val();
        skillstatus = $("#editstatus").val();
        $.ajax({
            type: "post",
            url: '/Admin/EditSkill',
            data: { id: skillId, status: skillstatus, skillName: skillnameedit },
            success: function (data) {
                alert("skill edited successfully");
            },
            error: function (xhr, status, error) {
                // Handle error
                alert("error");
            }

        });
    });

}
var skillidfordelete = "";
function deleteskill() {
    $(".deletebtn").on("click", function () {
        skillidfordelete = this.id;
        console.log(skillidfordelete);
        $("#deleteskill").on("click", function () {
            $.ajax({
                type: "post",
                url: '/Admin/DeleteSkill',
                data: { id: skillidfordelete },
                success: function (data) {
                    alert("skill deleted successfully");
                    location.reload();
                },
                error: function (xhr, status, error) {
                    // Handle error
                    alert("error");
                }

            });
        });
    })
}
