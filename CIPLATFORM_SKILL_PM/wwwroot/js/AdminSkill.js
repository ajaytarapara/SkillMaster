$(document).ready(function () {
    $("#searchskill").val(sessionStorage.getItem("searchtext"));
    $("#sortby").on("change", function () {
        var orderBy = $("#sortby").val();
        sessionStorage.setItem("sortby", orderBy);
        getSkillData()
    });
    $("#pagesize").on("change", function () {
        var pagesize = $("#pagesize").val();
        sessionStorage.setItem("pagesize", pagesize);
        getSkillData();
    });
    getSkillData();
    searchskill();
});
function getSkillData(pageNumber, orderBy, size) {
    $(".loader-div").show();
    if (pageNumber == null || pageNumber == "") {
        pageNumber = sessionStorage.getItem("pagenumber");
    }
    if (searchusertext == null || searchusertext == "") {
        searchusertext = sessionStorage.getItem("searchtext");
    }
    if (orderBy == null || orderBy == "") {
        orderBy = sessionStorage.getItem("sortby");
    }
    if (size == null || size == "") {
        pagesize = sessionStorage.getItem("pagesize");
    }
    $.ajax({
        type: "post",
        url: '/AdminSkill/GetSkillData',
        data: { page: pageNumber, searchText: searchusertext, orderBy: orderBy, size: pagesize },
        cache: true,
        dataType: "html",
        success: function (data) {
            setTimeout(() => {
            $("#skillTable").html("");
            $("#skillTable").html(data);
                $(".loader-div").hide();
            }, 500);
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    });
}
var skillId = "";
function editskill(SkillId, PageNumber) {
    $(".loader-div").show();
    $.ajax({
        type: "get",
        url: '/AdminSkill/getSkillforedit',
        data: { id: SkillId },
        success: function (data) {
            $("#editskillname").val(data.skillName);
            $("#editstatus").val("" + data.status);
            $("#EditSkillId").val("" + data.skillId);
            $("#editskill").attr("onclick", "editskillpost(" + SkillId + ", " + PageNumber + " )");
            $(".loader-div").hide();
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
    $(".loader-div").show();
    skillnameedit = $("#editskillname").val();
    skillstatus = $("#editstatus").val();
    $.ajax({
        type: "post",
        url: '/AdminSkill/AddEditSkill',
        data: { id: skillId, status: skillstatus, skillName: skillnameedit },
        success: function (data) {

            getSkillData(PageNumber);
            $(".loader-div").hide();
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
    $(".loader-div").show();
    $.ajax({
        type: 'POST',
        url: '/AdminSkill/DeleteSkill',
        data: { id: skillid },
        success: function (result) {
            $(".loader-div").hide();
            toastr.error("Skill deleted successfully");
            getSkillData();
        },
        error: function () {
            alert("error");
        }
    });
}
var searchusertext = "";
function searchskill() {
    $("#searchskill").on("keyup", function () {
        searchusertext = $("#searchskill").val();
        sessionStorage.setItem("searchtext", searchusertext);
        if (searchusertext.length > 1) {
            getSkillData();
        }
        else {
            searchusertext = "";
            getSkillData();
        }
    });
}
var skillname = "";
var status = "";
function addSkill() {
    $(".loader-div").show();
    skillname = $("#skillname").val();
    status = $("#status").val();
    $.ajax({
        type: "Post",
        url: '/AdminSkill/AddEditSkill',
        data: { skillname: skillname, status: status },
        success: function (data) {
            if (data["data"] == 1) {
                $('#EditskillModal').toggle();
                getSkillData();
            }
            $(".loader-div").hide();
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }

    });
}

