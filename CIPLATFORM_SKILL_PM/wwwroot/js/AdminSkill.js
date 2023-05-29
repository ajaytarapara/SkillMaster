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
            $("#skillTable").html("");
            $("#skillTable").html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    });
}

//function sortby(orderBy) {
//    var pagersize = sessionStorage.getItem("pagesize");
//    pageSize(pagersize);
//    sessionStorage.setItem("sortby", orderBy);
//   var pageNumber = sessionStorage.getItem("pagenumber");
//    getSkillData(pageNumber,orderBy);
//}

//function pageSize(size) {
//    sessionStorage.setItem("pagesize", size);
//    getSkillData(1,0,size);
//}

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
        url: '/AdminSkill/AddEditSkill',
        data: { id: skillId, status: skillstatus, skillName: skillnameedit },
        success: function (data) {
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
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }

    });
}

