//能源統計專區
//Roni 2020/12/01

//行動裝置css hack
if(navigator.userAgent.match(/iP(hone|od|ad)/i)){jQuery('html').addClass('ios')}
if(navigator.userAgent.match(/iP(hone|od)/i)){jQuery('body').addClass('iphone')}if(navigator.userAgent.match(/iPad/)){$('html').addClass('ipad')}
if (navigator.userAgent.match(/android/i)){jQuery('body').addClass('android')}
//IE11以下hack(not Eadge)
var version=detectIE();if(version===false){$("body").addClass("notie")}else{if(version>=12){}else{$("body").addClass("ie11")}}function detectIE(){var c=window.navigator.userAgent;var b=c.indexOf("MSIE ");if(b>0){return parseInt(c.substring(b+5,c.indexOf(".",b)),10)}var a=c.indexOf("Trident/");if(a>0){var e=c.indexOf("rv:");return parseInt(c.substring(e+3,c.indexOf(".",e)),10)}var d=c.indexOf("Edge/");if(d>0){return parseInt(c.substring(d+5,c.indexOf(".",d)),10)}return false};
if (navigator.userAgent.indexOf("MSIE 10.0") !== -1){$("body").addClass("ie10");}
if (navigator.userAgent.match(/firefox/i)){jQuery('body').addClass('moz')}
if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1){jQuery('body').addClass('chrome')}

/*jquery.preloadinator.min.js 載入頁面
!function(a){a.fn.preloadinator=function(n){"use strict";var i=a.extend({scroll:!1,minTime:0,animation:"fadeOut",animationDuration:400},n),e=this,t=(new Date).getTime();function o(){a(e)[i.animation](i.animationDuration,function(){!1===i.scroll&&a("body").css("overflow","auto")})}return!1===i.scroll&&a("body").css("overflow","hidden"),a(window).on("load",function(){if((new Date).getTime()-t>=i.minTime)o();else{var n=(new Date).getTime()-t;setTimeout(o,i.minTime-n)}}),this}}(jQuery);$('.js_preloader').preloadinator({minTime:1000});*/

//Rotated Resize(Tablet+Mobile)
//jQuery().ready(function(){function a(){var b=(window.innerWidth>0)?window.innerWidth:document.documentElement.clientWidth;if(b>1366){$(window).bind("resize",function(a){if(window.RT){clearTimeout(window.RT)}window.RT=setTimeout(function(){this.location.reload(false)},10)});}else{}}window.onresize=a;a()});if (navigator.userAgent.match(/Mobile|Windows Phone|Lumia|Android|webOS|iPad|iPhone|iPod|Blackberry|PlayBook|BB10|Opera Mini|\bCrMo\/|Opera Mobi/i)){if(window.DeviceOrientationEvent){window.addEventListener("orientationchange",function(){location.reload()},false)};}

//jquery.back-to-top.js回頂端
(function(a){a.fn.backToTop=function(c){var g=a.extend({fxName:"fade",fxTransitionDuration:500,scrollDuration:500},c);var e=this,f=g.iconName,b=g.trigger,d=g.fxName,h=g.fxTransitionDuration,i=g.scrollDuration;this.each(function(){e.addClass(d);e.css({transitionDuration:h+"webkit"});a(window).scroll(function(){if(window.pageYOffset>100){e.addClass("bck-on")}else{e.removeClass("bck-on")}});e.on("click",function(j){j.preventDefault();a("html,body").animate({scrollTop:0},i)})});return this}})(jQuery);$(function(){$(".backtotop").backToTop({fxName:"rightToLeft"})});

//Bootstrap4 Toggle animation
$(function(){$("#navbarResponsive").on("hide.bs.collapse",function(){$(".navbar-toggler").removeClass("open")}),$("#navbarResponsive").on("show.bs.collapse",function(){$(".navbar-toggler").addClass("open")})});

//桌機sub drop down，也可以用slideDown換展開方式
$(".dropdown").hover(function(){$(this).find(".dropdown-menu").stop(true,true).delay(200).slideDown(500)},function(){$(this).find(".dropdown-menu").stop(true,true).delay(200).slideUp(200)});

//手機submenu Animation
$(".dropdown").on("show.bs.dropdown",function(a){$(this).find(".dropdown-menu").first().stop(true,true).slideDown(300)});$(".dropdown").on("hide.bs.dropdown",function(a){$(this).find(".dropdown-menu").first().stop(true,true).slideUp(200)});

//資料庫Accordion Form
//如root active remove sub active也移除
$(".outer_selectall>.data_search_bar+.open_symbol").each(function(){$(this).on("click",function(){$(".outer_selectall_2>.data_search_bar+.open_symbol").removeClass("active")})});
//第1層Accordion(單一展開)
//$(function(){$(".outer_selectall>.data_search_bar+.open_symbol").click(function(){$(".outer_selectall>.data_search_bar+.open_symbol").removeClass("active");function a(){ua=navigator.userAgent;var b=ua.indexOf("MSIE ")>-1||ua.indexOf("Trident/")>-1;return b}if(a()){$(".outer_selectall .datasearch_box").slideUp('fast');}else{$(".outer_selectall .datasearch_box").slideUp()}if($(this).next().is(":hidden")==true){$(this).addClass("active");function a(){ua=navigator.userAgent;var b=ua.indexOf("MSIE ")>-1||ua.indexOf("Trident/")>-1;return b}if(a()){$(this).next().show()}else{$(this).next().slideDown()}}})});
//第1層Accordion(手動多層展開)
//$(".outer_selectall>.datasearch_box").show();$(".outer_selectall>.data_search_bar+.open_symbol").addClass("active");$(function(){$(".outer_selectall>.data_search_bar+.open_symbol").on("click",function(b){b.preventDefault();var a=$(this);var c=a.next(".outer_selectall .datasearch_box");a.toggleClass("active");c.slideToggle()})});

//第2層Accordion
$(function(){$(".outer_selectall_2>.data_search_bar+.open_symbol").click(function(){$(".outer_selectall_2>.data_search_bar+.open_symbol").removeClass("active");function a(){ua=navigator.userAgent;var b=ua.indexOf("MSIE ")>-1||ua.indexOf("Trident/")>-1;return b}if(a()){$(".datasearch_box.sub3").slideUp("fast")}else{$(".datasearch_box.sub3").slideUp(),$(this).removeClass("active")}if($(this).next().is(":hidden")==true){$(this).addClass("active");function a(){ua=navigator.userAgent;var b=ua.indexOf("MSIE ")>-1||ua.indexOf("Trident/")>-1;return b}if(a()){$(this).next().show()}else{$(this).next().slideDown()}}})});
//第3層Accordion
$(function(){$(".outer_selectall_3>.data_search_bar+.open_symbol").click(function(){$(".outer_selectall_3>.data_search_bar+.open_symbol").removeClass("active");function a(){ua=navigator.userAgent;var b=ua.indexOf("MSIE ")>-1||ua.indexOf("Trident/")>-1;return b}if(a()){$(".datasearch_box.sub4").slideUp("fast")}else{$(".datasearch_box.sub4").slideUp()}if($(this).next().is(":hidden")==true){$(this).addClass("active");function a(){ua=navigator.userAgent;var b=ua.indexOf("MSIE ")>-1||ua.indexOf("Trident/")>-1;return b}if(a()){$(this).next().show()}else{$(this).next().slideDown()}}})});



//全選
//$(".l1_2").click(function () {$("input:checkbox").prop('checked', $(this).prop("checked"));});
//第1層(深藍bar)
$(function () { $(".checkall").click(function () {$(this).closest(".outer_selectall").find(':checkbox').prop('checked', this.checked); $(this).closest(".datasearch_box").find(':checkbox').prop('checked', this.checked); }); });
//第2層(淺綠區)
$(function (){$(".checkall_2").click(function (){$(this).closest(".datasearch_box_2").find(':checkbox').prop('checked', this.checked);});});
//第3層(淺綠第2層)
$(function (){$(".checkall_3").click(function (){$(this).closest(".outer_selectall_3").find(':checkbox').prop('checked', this.checked);});});
//第4層第3層(淺綠第3層縮排區)
$(function (){$(".checkall_4").click(function (){$(this).closest(".outer_selectall_4").find(':checkbox').prop('checked', this.checked);});});


//簡易查詢 hover opacity
$(".easy_imglist>li>a").hover(function() { // Mouse over
  $(this).siblings().stop().fadeTo(300,1);
  $(this).parent().siblings().stop().fadeTo(300,.5); 
}, function() { // Mouse out
  $(this).siblings().stop().fadeTo(300,1);
  $(this).parent().siblings().stop().fadeTo(300,1);
});

//出版品 查詢系統Accordion
$(function(){
    $(".inquire_accordion>.panel").click(function(){
	$(this).parent().toggleClass("current");
	$(this).next().slideToggle()
	$(this).parent().siblings().find('.panel-content').slideUp(200)	
	$(this).parent().siblings().removeClass("current");
    });
});
//手機平板移動Y軸至頂端
var ismobile = /Android|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);if (ismobile){
 $(".inquire_accordion>.panel").click(function(){$(".inquire_accordion .panel-content").animate({scrollTop:50},80);return false;})
}

//關閉此頁
function Exit(){var a=confirm("確定要離開此頁嗎?");if(a){window.close()}};

/*出版品單頁select錨點
if(window.matchMedia("(min-width:992px)").matches){$(function(){$(".publication_anchor_drop select").on("change",function(c){c.preventDefault();var b=$(this).val();var a=$(b);$("html, body").animate({scrollTop:a.offset().top-170},800,"swing")}).change()})};
if(window.matchMedia("(max-width:991px)").matches){$(function(){$(".publication_anchor_drop select").on("change",function(c){var b=$(this).val();var a=$(b);$("html, body").animate({scrollTop:a.offset().top-150},800,"swing")}).change()})};*/

//tinynav出版品單頁select錨點
(function(a,k,g){a.fn.tinyNav=function(l){var c=a.extend({active:"selected",header:"",indent:"- ",label:""},l);return this.each(function(){g++;var h=a(this),b="m_ul"+g,f=".l_"+b,e=a("<select/>").attr("id",b).addClass("tinynav "+b);if(h.is("ul,ol")){""!==c.header&&e.append(a("<option/>").text(c.header));var d="";h.addClass("l_"+b).find("a").each(function(){d+='<option value="'+a(this).attr("href")+'">';var b;for(b=0;b<a(this).parents("ul, ol").length-1;b++)d+=c.indent;d+=a(this).text()+"</option>"});
e.append(d);c.header||e.find(":eq("+a(f+" li").index(a(f+" li."+c.active))+")").attr("selected",!0);e.change(function(){k.location.href=a(this).val()});a(f).after(e);c.label&&e.before(a("<label/>").attr("for",b).addClass("tinynav_label "+b+"_label").append(c.label))}})}})(jQuery,this,0);
$(function () {
      $('#publishing_fixselect').tinyNav({
        active: 'current',//要顯示current的位置
        indent: '&#9658;',//子目錄的前頭符號
       //label: 'Menu'
      }); });