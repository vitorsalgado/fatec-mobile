/// <reference path="Scripts/jquery-2.0.3.js" />
/// <reference path="Scripts/jquery-2.0.3.intellisense.js" />

var allowSearch = false;

$(document).ready(function () {
	$(window).bind("orientationchange resize pageshow", on_resize);

	$('#show-search').click(function () {
		if(allowSearch){
			$('#search-container').toggle();
		}

		if (document.getElementById('search-container').style.display != 'none') {
			document.getElementById('search-box').focus();
		}
	});
});

var tile_auto_resize_elements = $('.tile-auto-resize');

function on_resize() {
	adjustMainContainerHeight();
}

function adjustMainContainerHeight() {
	var header = $('#header');
	var container = $('#container');
	var footer = $('#footer');

	var viewPortHeight = $(window).height();

	var containerHeight = viewPortHeight - header.outerHeight() - footer.outerHeight();
	containerHeight -= (container.outerHeight() - container.height());

	container.height(containerHeight);
}

function adjustTilesHeight() {
	if (tile_auto_resize_elements == null)
		tile_auto_resize_elements = $('.tile-auto-resize');

	jQuery.each(tile_auto_resize_elements, function (index, value) {
		$(value).height($(value).width());
	});
}