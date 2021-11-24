function AjaxSend(form) {
    if ($("#txtTitle").val().length <= 10) {
        console.log(form.action)

        alert("Title can't less than 10 character!")
    }
    let data = new FormData(form);
    if (editor.getData().length >= 255) {
        data.set("article_content", editor.getData())

    } else {
        alert("Content can't less than 255 character!");
        return false;
    }
    if ($("#txtTitle").val().length >= 10 && editor.getData().length >= 255) {
        $.ajax({
            type: "POST",
            url: form.action,
            contentType: false,
            processData: false,
            beforeSend: () => {
                $(".preloader").fadeIn(1000);
            },
            data: data,
            success: (response) => {
                if (response) {
                    $(".preloader").fadeOut(1000);
                    setTimeout(() => {
                        alert("Add Article Success!")
                        window.location.assign(window.location.href)
                    }, 1000)
                } else {
                    alert("Add Faild Something err")
                }
            },
           
            error: () => {
                alert("Some thing err")
            }
        })
    }
    return false;
}

function updateArticle(form) {
    let flag = true
    if (confirm("Do you really update ? ")) {
        if ($("#txtTitle_up").val().length < 10) {
            alert("Title can't less than 10 character!");
            flag = false;
        } else if (editor_up.getData().length <= 255) {
            alert("Content can't be less than 255 character !");
        } else {
            flag = true;
        }
    }
    if (flag) {
        let data_ = new FormData(form);
        data_.set("id", $("#txtId").val())
        data_.set("article_content", editor_up.getData());
        $.ajax({
            type: "POST",
            url: form.action,
            contentType: false,
            processData: false,
            dataType: "json",
            data: data_,
            beforeSend: () => {
                $(".preloader").fadeIn();
            },
            success: (response) => {
                $(".preloader").fadeOut();
                if (response) {
                    setTimeout(() => {
                        alert("Update Success !");
                        window.location.assign(window.location.href);
                    }, 1000)
                }
            },
            error: () => {
                $(".preloader").fadeOut();
                alert("some err");
            }
        })
    }
    return false;
}
function loadDataForUpdate(data) {

    $("#txtTitle_up").val(data.result.article_title);
    editor_up.setData(data.result.article_content);
    $("#category_up").val(data.result.category_id);
    var thumSrc = data.result.article_thumbnail;
    thumSrc = thumSrc.slice(1, thumSrc.length);
    $("#currentThubm").attr("src", thumSrc);
    setTimeout(() => {
        $(".preloader").hide();
        $("#updateModal").modal("show");
    }, 1000)
}
function getDataToShowModal(article_id) {

    $("#updateModal").modal("hide");
    $(".preloader").show();
    $.ajax({
        type: "POST",
        url: "GetArticleData",
        dataType: "json",
        data: jQuery.param({ id: article_id }),
        success: (data) => {
            if (data != null) {
                var data_ = data;
                loadDataForUpdate(data_)
            }
            else {
                $(".preloader").hide();
                alert("Something err a");
            }
        },
        error: () => {
            $(".preloader").fadeOut(200);
            alert("Something err");
        }
    })
}


var editBtnAdd = $(".editor_article");
editBtnAdd.click((e) => {
    $("#txtId").val(e.target.dataset.id)
    getDataToShowModal(e.target.dataset.id);
})

var btnRemoveArt = $(".remove_article");
function removeArticle(article_id) {
    $.ajax({
        type: "POST",
        url: "RemovedArticle",
        data: jQuery.param({ id: article_id }),
        beforeSend: () => {
            $(".preloader").show();
        },
        success: (response) => {
            $(".preloader").hide();
            if (response) {
                window.location.assign(window.location.href);
            }
        }
    })
}
btnRemoveArt.click((e) => {

    if (confirm("Do You Really Remove this Article ?")) {
        removeArticle(e.target.dataset.id);
    }
})
function SearchArticle(page_) {
    let flag = false;
    let query = $("#searchString").val()
    if (query.length == 0) {
        alert("Value can't be null");
    } else {
        flag = true;
    }

    if (flag) {
        $.ajax({
            type: "POST",
            url: "ArticleSearch",
            data: jQuery.param({ queryStr: query, page: page_ }),
            beforeSend: () => {
                $(".preloader").show();
            },
            success: (data) => {
                if (data !== null) {
                    loadArticleToView(data, page_)
                    setTimeout(() => {
                        $(".preloader").hide();
                    }, 1000)

                }
            }
        });
    }
    return false;
}
function loadArticleToView(data, page) {
    let listArticle = $("#articleTable");
    let nav = $("#articlePagination");
    listArticle.empty();
    nav.empty();
    let context = ``;
    data.forEach((val) => {
    
        context = `
            <tr>
                <td>${val.id}</td>
                <td class="text-overflow">${val.article_title}</td>
                <td class="text-center"><img src="${getImagePath(val.article_thumbnail)}" width="60px" alt="article-cover-image" /></td>
                <td class="text-center">${convertDate(val.update_at)}</td>
                <td class="text-center">${val.view.total_view}</td>
                <td><span class="fa fa-pen-square editor_article" data-id="${val.id}" data-bs-toggle ="modal" data-bs-target ="#updateModal"> </span> <span class="fa fa-trash remove_article" data-id="${val.id}"> </span> <a href="@Url.Action('Index','NewDetails',new { id = ${val.id}})"><span class="fa fa-eye text-dark"></span></a></td>
            </tr>
            `;
        listArticle.append(context);
    })
    // page list
    let totalPage = getCountArtile();
    console.log(totalPage)
    let btnPre = ` `;
    let btnNext = ` `;
    let btnMid = ` `;
    if (page - 1 > 0) {
        btnPre = `
                <a href="#" onclick = "SearchCategory(${page - 1})"> <span aria-hidden="true">&laquo;</span> </a>
            `;
    }
    nav.append(btnPre);

    for (let i = 1; i <= totalPage; i++) {
        if (i == page) {
            btnMid = `
                <a href="#" onclick = "SearchCategory(${i})" class = "actived"> <span aria-hidden="true">${i}</span> </a>
            `;
        } else {
            btnMid = `
                <a href="#" onclick = "SearchCategory(${i})"> <span aria-hidden="true">${i}</span> </a>
            `;
        }
        nav.append(btnMid);
    }
    if (page + 1 <= totalPage) {
        btnNext = `
                <a href="#" onclick = "SearchCategory(${page + 1})"> <span aria-hidden="true">&raquo; </span> </a>
            `;
    }
    
    
}
function getCountArtile(count = 0) {
    $.ajax({
        type:"POST",
        url: "GetCountArticle",
        async: false,
        success: (data) => {
            var result = data.result;
            count = result;
        }
    })
    return count;
}
function getImagePath(imagePath) {
    let path = imagePath.slice(1, imagePath.length)
    console.log(path);
    return path;
    
}
//category 
function getTotalArticle() {

}
function addCategody(form) {
    let flag = true
    if ($("#txtName").val().length < 2) {
        flag = false;
        alert("Category name can't less than  2 character!");
    } else {
        flag = true;
    }
    if (flag) {
        let data_ = new FormData(form);
        $.ajax({
            type: "POST",
            url: form.action,
            processData: false,
            contentType: false,
            data: data_,
            beforeSend: () => {
                $(".preloader").show();
            },
            success: (reponse) => {
                if (reponse) {
                    setTimeout(() => {
                        $(".preloader").hide();
                    }, 1000)
                    setTimeout(() => {
                        
                        alert("Add Success!");
                        window.location.assign(window.location.href);
                    }, 1200)
                 
                }
            }
            
        })
    }
    return false;
}
function loadDataForUpdateCategory(category_id) {
    $.ajax({
        type: "POST",
        url: "GetCategoryData",
        dataType: "json",
        data: jQuery.param({ id: category_id }),
        beforeSend: () => {
            $(".preloader").show();
        },
        success: (data) => {
            if (data != null) {
                setTimeout(() => {
                    $("#txtCategoryName").val(data.reuslt.category_name);
                    $(".preloader").hide();
                    $("#updateModal").modal("show");
                }, 1000)
               
            }
        }, error: () => {
            $(".preloader").hide();
            alert("Err")
        }
    })
}
function updateCategory(form) {
    let flag = false;
    if ($("#txtCategoryName").val().length <= 4) {
        alert("Category name's can't less than 4 character!");
    } else {
        flag = true;
    }
    if (flag) {
        let data_ = new FormData(form);
        data_.set("id", $("#txtCategoryId").val())
        console.log(data_.get("id"))
        $.ajax({
            type: "POST",
            url: "UpdateCategory",
            processData: false,
            contentType: false,
            data: data_,
            beforeSend: () => {
                $(".preloader").show();
            },
            success: (response) => {
                if (response) {
                    setTimeout(() => {
                        $(".preloader").hide();
                       
                    }, 1000)
                    setTimeout(() => {
                        alert("Update success!");
                        window.location.assign(window.location.href);
                    },1200)
                } else {
                    setTimeout(() => {
                        $(".preloader").hide();
                        alert("Something err!");
                        window.location.assign(window.location.href);
                    }, 1000)
                }
            }
        })
    }
    return false;
}
function removeCategory(category_id) {
    if (confirm("Do you really remove this category?")) {
        $.ajax({
            type: "POST",
            url: "RemoveCategory",
            data: jQuery.param({ id: category_id }),
            beforeSend: () => {
                $(".preloader").show();

            },
            success: (response) => {
                if (response) {
                    setTimeout(() => {
                        $(".preloader").hide();
                    },1000)
                    setTimeout(() => {
                        alert("Remove Success!");
                        window.location.assign(window.location.href);
                    }, 1200)

                }
            }
        })
    }
}
$(".editor_category").click((e) => {
    $("#updateModal").modal("hide");
    $("#txtCategoryId").val(e.target.dataset.id);
    loadDataForUpdateCategory(e.target.dataset.id);
});
$(".remove_category").click((e) => {
    removeCategory(e.target.dataset.id);
});
//
//search category
function loadDataToView(data,page) {
    let listCategories = $("#categoryTable");
    listCategories.empty();
    let nav = $("#categoryPagination");
    nav.empty();
    let context = " ";
    data.result.forEach((value) => {
          context = `
           <tr>
              <td>${value.id}</td>
              <td class="text-overflow">${value.category_name}</td>
              <td class="text-center">${convertDate(value.update_at)}</td>
              <td class="text-center">${convertDate(value.create_at)}</td>
              <td><span class="fa fa-pen-square editor_category pointer" data-id="${value.id}" data-bs-toggle="modal" data-bs-target="#updateModal"> </span> <span class="fa fa-trash remove_category pointer" data-id="${value.id}"> </span> <span class="fa fa-eye pointer"></span></td>
          </tr>
      `;
      listCategories.append(context);
    })
    $(".editor_category").click((e) => {
        $("#updateModal").modal("hide");
        $("#txtCategoryId").val(e.target.dataset.id);
        loadDataForUpdateCategory(e.target.dataset.id)
    });
    $(".remove_category").click((e) => {
        removeCategory(e.target.dataset.id);
    });

    let totalPage = GetCountCategory();
    console.log(totalPage)
    let btnPre = ` `;
    let btnNext = ` `;
    let btnMid = ` `;
    if (page - 1 > 0) {
        btnPre = `
                <a href="#" onclick = "SearchCategory(${page - 1})"> <span aria-hidden="true">&laquo;</span> </a>
            `;
    }
    nav.append(btnPre);
    
    for (let i = 1; i <= totalPage; i++) {
        if (i == page) {
            btnMid = `
                <a href="#" onclick = "SearchCategory(${i})" class = "actived"> <span aria-hidden="true">${i}</span> </a>
            `;
        } else {
            btnMid = `
                <a href="#" onclick = "SearchCategory(${i})"> <span aria-hidden="true">${i}</span> </a>
            `;
        }
        nav.append(btnMid);
    }
    if (page + 1 <= totalPage) {
        btnNext = `
                <a href="#" onclick = "SearchCategory(${page + 1})"> <span aria-hidden="true">&raquo; </span> </a>
            `;
    }
    nav.append(btnNext);

}
function GetCountCategory(count = 0) {
    $.ajax({
        type: "POST",
        url: "GetTotalCategory",
        async: false,
        success: (data) => {
            var result = data.result;
            count = result;
        }
    })
    console.log(count);
    return count;
}
function convertDate(dateStr) {
    dateStr = dateStr.slice(6, dateStr.length - 2);
    let date = new Date(parseInt(dateStr));
    return date.toUTCString();
}

function SearchCategory(page_) {
    let flag = false;
    let query = $("#searchString").val()
    if (query.length == 0) {
        alert("Value can't be null");
    } else {
        flag = true;
    }

    if (flag) {
        $.ajax({
            type: "POST",
            url: "CategorySearch",
            data: jQuery.param({ queryStr: query , page : page_ }),
            beforeSend: () => {
                $(".preloader").show();
            },
            success: (data) => {
                if (data !== null) {
                    loadDataToView(data,page_);
                    setTimeout(() => {
                        $(".preloader").hide();
                    }, 1000)

                }
            }
        });
    }
    return false;
}
$(function () {
    "use strict";
    // ============================================================== 
    // Newsletter
    // ============================================================== 
    var totalUser = $("#totalUsers").data("value");
    var totalArticle = $("#totalArticles").data("value");
    var totalComment = $("#totalComments").data("value");
    var new_series = [];
    for (var j = 0; j < 3; j++) {
        var change = totalArticle;
        if (j > 0) { change = totalUser;} else if (j > 1) { change = totalComment; }
        var new_array = [];
        for (var i = 0; i <= 7; i++) {
            new_array.push(Math.floor(Math.random() * change) + 1)
        }
        new_series.push(new_array);
    }
    var data = {
        labels: ['2014', '2015', '2016', '2017', '2018', '2019', '2020', '2021'],
        series: new_series
    };
    var option = {
        top: 0,
        low: 1,
        showPoint: true,
        fullWidth: true,
        plugins: [
            Chartist.plugins.tooltip()
        ],
        axisY: {
            labelInterpolationFnc: function (value) {
                return (value / 1) + 'k';
            }
        },
        showArea: true
    };
    //ct-visits
    new Chartist.Line('#ct-visits', data, option);


    var chart = [chart];

    var sparklineLogin = function () {
        $('#sparklinedash').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
            type: 'bar',
            height: '30',
            barWidth: '4',
            resize: true,
            barSpacing: '5',
            barColor: '#7ace4c'
        });
        $('#sparklinedash2').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
            type: 'bar',
            height: '30',
            barWidth: '4',
            resize: true,
            barSpacing: '5',
            barColor: '#7460ee'
        });
        $('#sparklinedash3').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
            type: 'bar',
            height: '30',
            barWidth: '4',
            resize: true,
            barSpacing: '5',
            barColor: '#11a0f8'
        });
        $('#sparklinedash4').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
            type: 'bar',
            height: '30',
            barWidth: '4',
            resize: true,
            barSpacing: '5',
            barColor: '#f33155'
        });
    }
    var sparkResize;
    $(window).on("resize", function (e) {
        clearTimeout(sparkResize);
        sparkResize = setTimeout(sparklineLogin, 500);
    });
    sparklineLogin();
});
var editor = CKEDITOR.replace('articleContent_add', {
    customConfig: '/Public/Admin/plugins/ckeditor/config.js',
});
var editor_up = CKEDITOR.replace('articleContent_up', {
    customConfig: '/Public/Admin/plugins/ckeditor/config.js',
});