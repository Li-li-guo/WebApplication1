// @ts-nocheck
//var MyAjax = function (ul, callback) {
//	var arry = new Array();
//	$.ajax(ul, {
//		dataType: 'json',
//		async: false
//	}).done(function (data) {
//		callback(data);
//		//						console.log("========>"+shuju);
//	});
//}
////				var arry = new Array();
//				$.ajax({
//					type: "GET",
//					url: "c.json",
//					dataType: "json",
//					success: function(data) {
//						
//						$.each(data.c, function(i, item) {
//							var str = item.name + "," + item.advantage + ";";
//							arry = [item.name, item.advantage, item.disadvantage, item.country, item.recommend_models];
//							shuju.push(arry);
//						});
//						console.log(shuju.length);
//						return shuju;
//					}
//			          console.log(shuju.length);
//					//				console.log(shuju.length);
//				});
//根据数据长度生成页码并添加到HTML页面
function initBtnPage(pageAll) {
	$("#changeMyPage").append(
		"<div style='float: left'>" +
		//在页面生成一个按钮并绑定changeNumPage方法
		"<input type='button'onclick='changeNumPage($(this).val())' value='上一页'/>" +
		"</div>" +
		"<div id='page_navigation' style='float: left;'>" +
		"</div>" +
		"<div style='float: left;'>" +
		"<input type='button' id='down_page' onclick='changeNumPage($(this).val())' value='下一页' />" +
		"</div>");
	//根据总页数生成页码添加到页面
	for (var i = 1; i < pageAll; i++) {
		$("#page_navigation").append("<input type='button'onclick='changeNumPage($(this).val())' id='pageNumber" + i + "'  value='" + i + "'/>");
		//						$("#page_navigation input").eq(i-1).bind("click",'changepage($(this).text())');
	}
	//					$("#changepage input").eq(0).click(changepage($(this).val()));
}
//初始化页面
function initTable(pagetart, pagelast) {
	//移除数据所在的div
	$("#con div").remove();
	for (var i = 0; i < shuju.length; i++) {
		//判断i是否在每页数据下标范围内
		if (i >= pagetart && i < pagelast) {
			//			console.log(pagelast);
			//			console.log(shuju.length);
			//将表头样式复制到数据所在div
			var div = $("#typem").clone();

			//将数据添加到div
			for (j = 0; j <= shuju[i].length; j++) {
				if (j == 0) {
					//将checkbox移除
					div.children().eq(j).children().remove();
					//添加input
					div.children().eq(j).html("<input type='checkbox'class='checkboxeds'onclick='allCheckboxNone()' id='Checkboxs" + i + "'/>");
					//div.children().eq(j).append("<input type='checkbox' id="+i+"/>");
				} else {
					if (j == 1) {
						div.children().eq(j).attr("id", i);
						div.children().eq(j).text(shuju[i][j - 1]);
						div.children().eq(j).bind("click", function () {
							// console.log(this.id);
							for (var i = 0; i < shuju[this.id].length; i++) {
								$(".ViewValue").eq(i).val(shuju[this.id][i]);
								// console.log(shuju[this.id][i]);
							}
							$("#bg").attr("style", "z-index: 2;");
							$("#ty").attr("style", "z-index: 3;");
						});
					}
					//将shuju中的数据添加到div当中
					if (i % 2 == 0) {
						div.addClass("class");
					}
					div.children().eq(j).text(shuju[i][j - 1]);
				}
			}
			console.log(localStorage + "dfssgsgsg");
			//在#con里面结尾添加div
			$("#con").append(div);
		}
	}
}


//分页
function changeNumPage(valus) {
	console.log(valus);
	switch (valus) {
		case "上一页":
			//判断当前页起始下标是否满足条件
			if (pageStart <= 0) { } else {
				pageStart -= perLength;
				pageLast -= perLength;
				$("#con div").remove();
				initTable(pageStart, pageLast);
			}
			break;
		case "下一页":
			//判断当前页最后下标是否满足条件	
			if (pageLast >= shuju.length) { } else {
				pageStart += perLength;
				pageLast += perLength;
				$("#con div").remove();
				initTable(pageStart, pageLast);
			}
			break;
		default:
			pageStart = (valus - 1) * perLength;
			pageLast = pageStart + 4;
			$("#con div").remove();
			initTable(pageStart, pageLast);
			break;
	}
	// console.log(pageStart, pageLast);

	//							initTable(pageStart, pageLast);
}
//搜索
function searchHg() {
	$("#con div").remove();
	for (var i = 0; i < shuju.length; i++) {
		//定义一个变量
		var str = JSON.stringify(shuju[i]);
		//		console.log("shuju" + i + ":" + str);
		var tp = $("#guanjianzi").val();
		// console.log(tp);
		if (str.indexOf(tp) >= 0) {
			// console.log("search" + shuju[i]);
			var div = $("#typem").clone();
			for (j = 0; j <= shuju[i].length; j++) {
				if (j == 0) {
					//					div.children().eq(j)
				} else {
					div.children().eq(j).text(shuju[i][j - 1]);
				}
			}
			$("#con").append(div);
		}
	}
}

//localStorage.setItem("workjason", json, stringify(newdata));
//localStorage.workjson = JSON.stringify(newdata);
//localStorage["workjson"] = JSON.stringify(newdata);

window.onkeydown = function (e) {
	if (e && e.keyCode == 13) {
		//	searchHg();
		searchHg();
	}
}

//function KeyDown(event) {
//	if(e && e.keyCode == 13) {
//		//	searchHg();
//		alert("fuvk");
//	}
//}

function checkall() {
	var check = document.getElementsByClassName("checkboxeds");
	for (var i = 0; i < check.length; i++) {
		if (check[0].checked) {
			if (check[i].checked == false) {
				check[i].checked = true;
			} else {
				check[i].checked = true;
			}
		}
	}
	//	console.log(checkboxeds);
	//	for(i = 0; i < checkboxeds.length; i++) {
	//		checkboxeds[i].checked = document.getElementsByClassName("checkboxeds").checked;
	//	}
}

function allCheckboxNone() {
	var check = document.getElementsByClassName("checkboxeds");
	console.log(check.length);
	//	check.checked=t
	for (var k = 0; k < check.length; k++) {
		if (check[k].checked == true) { } else {
			check[0].checked = false;
		}
	}
}

function deletedata() {
	var checkboxeds = document.getElementsByClassName("checkboxeds");
	for (var k = 0; k < checkboxeds.length; k++) {
		if (checkboxeds[k].checked) {
			checkboxeds[k].parentNode.parentNode.parentNode.removeChild(checkboxeds[k].parentNode.parentNode);
			k -= 1;
		}
	}
}

function removearraybyindex() {
	if (index <= (shuju.length - 1)) {
		for (var i = index; i < shuju.length; i++) {
			shuju[i] = shuju[i + 1]
		}
	} else {
		throw new Error('超出最大索引');
	}
	shuju.length = shuju.length - 1;
	return shuju;
}

function delda() {
	var checkboxeds = document.getElementsByClassName("checkboxeds");
	var delcount = 0;
	for (k = 0; k <= perLength - 1; k++) {
		if (dataindex > shuju.length) {
			if (shuju.length <= perLength) { }
			nowpage -= 1;
			break;
		}
		if (checkboxeds[k].checked) {
			shuju = removearraybyindex(shuju, perLength * (nowpage - 1) + k - delcount);
			delcount += 1;
		}
	}
	dataindex = perLength * (nowpage - 1);
	bind.shuju();
}