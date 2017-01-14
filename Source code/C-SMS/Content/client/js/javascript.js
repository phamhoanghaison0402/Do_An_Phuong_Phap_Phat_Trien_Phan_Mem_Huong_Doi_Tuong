// Check xem trình duyệt là IE6 hay IE7
var isIE		= (navigator.userAgent.toLowerCase().indexOf("msie") == -1 ? false : true);
var isIE6	= (navigator.userAgent.toLowerCase().indexOf("msie 6") == -1 ? false : true);
var isIE7	= (navigator.userAgent.toLowerCase().indexOf("msie 7") == -1 ? false : true);
var ie45,ns6,ns4,dom;
if (navigator.appName=="Microsoft Internet Explorer") ie45=parseInt(navigator.appVersion)>=4;
else if (navigator.appName=="Netscape"){  ns6=parseInt(navigator.appVersion)>=5;  ns4=parseInt(navigator.appVersion)<5;}
dom=ie45 || ns6;

function getobj(id) {
	el =  document.getElementById(id);
	return el;
}
function showobj(id) {
obj=getobj(id);
els = dom ? obj.style : obj;
 	if (dom){
	    els.display = "";
    } else if (ns4){
        els.display = "show";
  	}
}

function hideobj(id) {
obj=getobj(id);
els = dom ? obj.style : obj;
 	if (dom){
	    els.display = "none";
    } else if (ns4){
        els.display = "hide";
  	}
}
// khong the phong to cua so
function openPopUp(url, windowName, w, h, scrollbar) {
   var winl = (screen.width - w) / 2;
   var wint = (screen.height - h) / 2;
   winprops = 'height='+h+',width='+w+',top='+wint+',left='+winl+',scrollbars='+scrollbar ;
   win = window.open(url, windowName, winprops);
   if (parseInt(navigator.appVersion) >= 4) { 
       	win.window.focus(); 
   } 
}

// co the phong to cua so
var win=null;
function NewWindow(mypage,myname,w,h,scroll,pos){
if(pos=="random"){LeftPosition=(screen.width)?Math.floor(Math.random()*(screen.width-w)):100;TopPosition=(screen.height)?Math.floor(Math.random()*((screen.height-h)-75)):100;}
if(pos=="center"){LeftPosition=(screen.width)?(screen.width-w)/2:100;TopPosition=(screen.height)?(screen.height-h)/2:100;}
else if((pos!="center" && pos!="random") || pos==null){LeftPosition=0;TopPosition=20}
settings='width='+w+',height='+h+',top='+TopPosition+',left='+LeftPosition+',scrollbars='+scroll+',location=no,directories=no,status=no,menubar=no,toolbar=no,resizable=yes';
win=window.open(mypage,myname,settings);}

// is_num
function is_num(event,f){
	if (event.srcElement) {kc =  event.keyCode;} else {kc =  event.which;}
	if ((kc < 47 || kc > 57) && kc != 8 && kc != 0) return false;
	return true;
}

// bookmarksite 
function bookmarksite(title,url){
	if (window.sidebar) // firefox
		window.sidebar.addPanel(title, url, "");
	else if(window.opera && window.print){ // opera
		var elem = document.createElement('a');
		elem.setAttribute('href',url);
		elem.setAttribute('title',title);
		elem.setAttribute('rel','sidebar');
		elem.click();
	} 
	else if(document.all)// ie
		window.external.AddFavorite(url, title);
}

// setHomepage 
function setHomepage(url)
{
	if (document.all)	{
		document.body.style.behavior='url(#default#homepage)';
		document.body.setHomePage(url);	
	}
	else if (window.sidebar)
	{
		if(window.netscape)
		{
			try			{
			netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
			}
			catch(e)			{
			alert("this action was aviod by your browser if you want to enable please enter about:config in your address line,and change the value of signed.applets.codebase_principal_support to true");
			}
		}
		var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components. interfaces.nsIPrefBranch);
		prefs.setCharPref('browser.startup.homepage',url);
	}
} 

 
/*--------------- Link advertise ----------------*/
 window.rwt = function (obj, type, id) {
	try {
		if (obj === window) {
			obj = window.event.srcElement;
			while (obj) {
				if (obj.href) break;
				obj = obj.parentNode
			}
		}		 
		obj.href = ROOT+'?'+cmd+'=mod:advertise|type:'+type+'|lid:'+id;
		obj.onmousedown = ""
	} catch(o) {}
	return true ;
};

function load_Statistics ()
{
	$.ajax({
		async: true,
		dataType: 'json',
		url: ROOT+"load_ajax.php?do=statistics",
		type: 'POST',
		success: function (data) {
			$("#stats_online").html(data.online);
			$("#stats_totals").html(data.totals);
			$("#stats_member").html(data.mem_online);
		}
	}) ; 
	 
} 


function show_popupBanner ()
{	 
	var mydata =  "lang=vn"  ; 
	$.ajax({
		async: true,
		dataType: 'json',
		url: ROOT+'load_ajax.php?do=popupBanner',
		type: 'POST',
		data: mydata ,
		success: function (data) {
			if(data.show==1)
			{
				vnTRUST.show_overlay_popup('popupBanner', '', data.html,
				{
					background: {'background-color' : 'transparent'},
					border: {
						'background-color' : 'transparent',
						'padding' : '0px'
					},
					title: {'display' : 'none'},
					content: {
						'padding' : '0px',
						'width' : data.width+'px'
					} ,
					pos_type: 'fixed',
					position : 'center-center'
				}); 
			}
		}
	}) ; 			
	 
}



function showBannerReview() {
	if($.cookie('bannerRV')) return false;
	var bannerRV = $('.rv-banner');
	var headerNav = $('.header_nav');
	var offsetHeader = headerNav.offset();
	if(!bannerRV.length) return false;
	//var offsetBannerRV = bannerRV.offset();
	jQuery(window).scroll(function(){
		var scrollTop  = $(window).scrollTop();
		if($.cookie('bannerRV')) return false;
		if(scrollTop > offsetHeader.top){        
			bannerRV.addClass('show');
		} else {        
			bannerRV.removeClass('show');
		}
	})
	$('.btn-close', bannerRV).bind('click', function(event) {   
			bannerRV.removeClass('show'); 
		$.cookie('bannerRV', 'true', { expires: (1/24)/2 });
	});
}

  function stickyNavMain() {
    var headerNav = $('.header_nav');
    var offsetHeader = headerNav.offset();
    jQuery(window).scroll(function(){
      var scrollTop  = $(window).scrollTop();
      if(scrollTop > offsetHeader.top + 10){
        headerNav.addClass('fixed');
      }else{
        headerNav.removeClass('fixed');
      }
    })
  }
	
	function backTop(){
    var backTop = $('.back-top');
    jQuery(window).scroll(function(){
      var scrollTop  = $(window).scrollTop();
      if(scrollTop > 500){
        backTop.addClass('on');
      }else{
        backTop.removeClass('on');
      }
    })

    backTop.click(function(event) {
      $('body, html').animate({
        scrollTop: 0
      }, 800);
    });
  }
	
	/* TOP MENU HOVER */
function hoverTopVideoMenu(linkObj) {
	var $linkObj = $(linkObj);
	
	if ($linkObj.length <= 0) return;
	if ($linkObj.data('target') == '' || $linkObj.data('target') == undefined) return;

	var $target = $($linkObj.data('target'));
	if ($target.length <= 0) return;

	$linkObj.on('mouseover', function(e) {
		e.stopPropagation();
		$linkObj.addClass('aover');
		$target.show();  

	});
	$('.link-videopage').on('mouseleave', function(e) {
		e.stopPropagation();
		$linkObj.removeClass('aover');
		$target.hide();
	});
}


$(document).ready(function(){

  $('.dropdown li.menu').hover(function(){
    $(this).addClass('hover');
    $('ul:first',this).css('visibility', 'visible');
  },function(){
    $(this).removeClass('hover');
    $('ul:first',this).css('visibility', 'hidden');
  });


  //$('.dropdown li ul').masonry({singleMode: true, columnWidth: 191});


  var open = true;
  $('.btn_support').click(function() {
    if(open === false) {
      $('.box_support').animate({ right : '-223px'  });
      open = true;

    } else {
      $('.box_support').animate({ right : '0px' });
      open = false;
    }
  });



  $(".div-user").mouseover(function(){
    $(this).addClass("open");
    ob	= $(this).find(".div_member") ;
    if(ob.is(':hidden')) {
      ob.show();
    }
  }).mouseout(function(){
    $(this).removeClass("open");
    $(this).find(".div_member").hide();
  });

  $('[data-toggle="tooltip"]').tooltip();


  $("#mmenu").mmenu({
    extensions: ["theme-dark"],
    "offCanvas": {
      "position": "left"
    }
  });

  jQuery('.icon-search').click(function(){
    jQuery('.headert_search').slideToggle('fast');
    $("#keyword_su").focus();
  });



});




function doPostBDS(ref) {
  if(mem_id==0){
    jAlert('Vui lòng đăng nhập thành viên','Báo lỗi',function() {popupLogin(ROOT+"member/dang-tin.html");});
  }else{
    window.location=ref;
  }
}



function PopupColorBox (obj,w,h)
{
  $("#"+obj).colorbox({iframe:true, maxWidth:w+"px", maxHeight:h+"px",width: '90%', height: '90%'});

}


function popupLogin(ref)
{
  $.colorbox({ iframe:true, maxWidth:"480px", maxHeight:"450px" ,width: '90%', height: '90%', scrolling:false ,href:ROOT+"member/popup.html/?do=login&ref="+ref , overlayClose:false , fixed : true ,onLoad: function() {  }, onComplete: function() {   }});
}

function popupRegister()
{
  $.colorbox({ iframe:true, maxWidth:"480px", maxHeight:"480px" ,width: '90%', height: '90%', scrolling:false , href:ROOT+"member/popup.html/?do=register" ,overlayClose:false , fixed : true ,onLoad: function() {  } , onComplete: function() {   }});
}

function doLogout()
{
  $.alerts.overlayColor = "#000000" ;
  $.alerts.overlayOpacity = "0.8" ;
  jConfirm('Bạn có muốn thoát khỏi hệ thống không ?', 'Xác nhận', function(r) {
    if (r){
      vnTRUST.ajax_popup('do=ajax_logout',"POST",{},
        function (j) {
          location.reload();
        });
    }
  });

  return false;
}


function popupFeedback()
{
  $.colorbox({ iframe:true, maxWidth:"550px", maxHeight:"630px" ,width: '90%', height: '90%', scrolling:true ,href:ROOT+"gioi-thieu/popup.html/?do=feedback" , overlayClose:true , fixed : true ,onLoad: function() {  }, onComplete: function() {   }});
}

//touch device mobile
document.addEventListener("touchstart", function(){}, true);



function getLoad_Films() {
  var options = {
    script: ROOT + "modules/product/ajax/ajax_list_search.php?json=true&do=product&lang=vn&",
    varname: "input",
    json: true,
    shownoresults: false,
    maxresults: 6,
    callback: function (obj) {
      document.getElementById('f_id').value = obj.id;
    }
  };
  var as_json = new bsn.AutoSuggest('keyword_su', options);
}


function initLoaded(){	 
	load_Statistics();
	stickyNavMain();
	backTop();
	hoverTopVideoMenu('.lik-vidpage');
}


var beginTime, endTime;
function CheckTimer() {
  var n = new Date;
  beginTime == 0 && endTime == 0 && (beginTime = n.getTime());
  return
}

function SuggestSearch(n) {
  var i = "#SuggestSearch ul li",
    r = new Date,
    t;
  if (endTime = r.getTime(), beginTime = endTime, n.which == 40) {
    $(i + ".selected").length == 0 ? $(i + ":first").addClass("selected") : (t = $(i + ".selected").next(), t.hasClass("li-group") && (t = t.next()), $(i + ".selected").removeClass("selected"), t.addClass("selected"));
    return
  }
  if (n.which == 38) {
    $(i + ".selected").length == 0 ? $(i + ":last").addClass("selected") : (t = $(i + ".selected").prev(), t.hasClass("li-group") && (t = t.prev()), $(i + ".selected").removeClass("selected"), t.addClass("selected"));
    return
  }
  SetTimeer(1)
}


function SetTimeer(n) {
  CallSuggestPerTimer(n)
}

function CallSuggestPerTimer(n) {
  setTimeout(function() {
    var i = new Date,
      t = i.getTime();
    if (beginTime == endTime) {
      if (t - endTime < 750 && t - endTime >= 0) {
        SetTimeer(n);
        return
      }
      if (t - endTime >= 750) {
        CallSuggest();
        return
      }
    } else return
  }, n)
}

function CallSuggest() {
  var i = $("#search-keyword").val().replace(/:|;|!|@@|#|\$|%|\^|&|\*|'|"|>|<|,|\.|\?|\/|`|~|\+|=|_|\(|\)|{|}|\[|\]|\\|\|/gi, ""),
    n = i.trim().toLowerCase(),
    t = "#SuggestSearch";
  if (n.length < 2) {
    $(t).hide();
    return
  }
  n.length >= 2 && (typeof searchpage != "undefined" && searchpage == 7 ? (n = n.trim().replace(/%20/gi, "+"), n = n.trim().replace(/ /gi, "+"),
    $.ajax({
    url: ROOT + "modules/product/ajax/ajax_list_search.php?do=product",
    type: "GET",
    data: {
      Key: n
    },
    cache: !0,
    success: function(data) {
      if(data.ok){
        $(t).html(data.list).show()
      }else{
        $(t).hide()
      }
    }
  })) : $.ajax({
    url: ROOT + "modules/product/ajax/ajax_list_search.php?do=product",
    type: "GET",
    data: {
      Key: n
    },
    cache: !0,
    success: function(data) {
      if(data.ok){
        $(t).html(data.list).show()
      }else{
        $(t).hide()
      }
    }
  }))
}
