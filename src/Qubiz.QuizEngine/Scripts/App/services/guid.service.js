(function () {
	'use strict'

	quizApp.factory('guid', guid);

	function guid() {
		return {
			newGuid: function () {

				var time = new Date().getTime();
				var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
					var r = (time + Math.random() * 16) % 16 | 0;
					time = Math.floor(time / 16);
					return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
				});
				return uuid;
			}
		}
	}
})()