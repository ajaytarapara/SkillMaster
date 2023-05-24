$(document).ready(function () {
    getSkillData();
    searchskill();
});

function getSkillData(pageNumber) {
    var orderBy = 0;
    $.ajax({
        type: "post",
        url: '/AdminSkill/GetSkillData',
        data: { page: pageNumber, searchText: searchusertext, orderBy:orderBy },
        dataType: "html",
        success: function (data) {
            $("#skillTable").html("");
            $("#skillTable").html(data);
            console.log(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    });
}

var skillId = "";
function editskill(SkillId, PageNumber) {
    $.ajax({
        type: "get",
        url: '/AdminSkill/getSkillforedit',
        data: { id: SkillId },
        success: function (data) {
            $("#editskillname").val(data.skillName);
            $("#editstatus").val("" + data.status);
            $("#EditSkillId").val("" + data.skillId);
            $("#editskill").attr("onclick", "editskillpost(" + SkillId + ", " + PageNumber + " )");
        },
        error: function (xhr) {
            // Handle error
            alert(xhr.responseText);
        }

    });
}

var skillnameedit = "";
var skillstatus = "";
function editskillpost(skillId, PageNumber) {
    skillnameedit = $("#editskillname").val();
    skillstatus = $("#editstatus").val();
    $.ajax({
        type: "post",
        url: '/AdminSkill/EditSkill',
        data: { id: skillId, status: skillstatus, skillName: skillnameedit },
        success: function (data) {
            if (data["data"] == 1)
                $('#EditskillModal').modal('toggle');
            getSkillData(PageNumber);
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }

    });
}

function confirmationForDelete(deleteId, PageNumber) {
    $("#delete-skill-id").attr("onclick", "deleteSkill(" + deleteId + ",' + @Model.PageNumber + ')");
}
function deleteSkill(skillid, pageNo) {
    $.ajax({
        type: 'POST',
        url: '/AdminSkill/DeleteSkill',
        data: { id: skillid },
        success: function (result) {
            toastr.error("Skill deleted successfully");
            getSkillData(pageNo);
        },
        error: function () {
            alert("error");
        }
    });
}
var searchusertext = "";
function searchskill() {
    $("#searchskill").on("keyup", function () {
        console.log("click");
        searchusertext = $("#searchskill").val();
        console.log("VALUE:" + searchusertext);
        if (searchusertext.length > 1) {
            getSkillData(1);
        }
        else {
            searchusertext = "";
            getSkillData(1);
        }
    });
}
var skillname = "";
var status = "";
function addSkill() {
    skillname = $("#skillname").val();
    console.log(skillname);
    status = $("#status").val();
    console.log(status);
    $.ajax({
        type: "Post",
        url: '/AdminSkill/AdminSkillAdd',
        data: { skillname: skillname, status: status },
        success: function (data) {
            if (data["data"] == 1) {
                $('#EditskillModal').toggle();
                getSkillData(1);
                location.reload();
            }
            else {
                toastr.error("Skill already exists");
            }
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }

    });
}