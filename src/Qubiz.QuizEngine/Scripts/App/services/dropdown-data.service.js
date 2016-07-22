(function () {
	'use strict'

	quizApp.factory('dropdownData', dropdownData);

    dropdownData.$inject = ['QuestionType'];

	function dropdownData (QuestionType){
        var Types = [
	        { content: "All Types", type: ""},
	        { content: "Single Choice", type: QuestionType.SingleSelect },
	        { content: "Multiple Choice", type : QuestionType.MultipleSelect }
	    ];

        var Complexities = [
	        { content: "All Complexities", type: ""}
	    ];

        for (var i = 0; i <= 10; i++) {
            var obj = { content: i, type: i};
	        Complexities.push(obj);
	    }
        
		return {
		    getTypes: function () {
		        
                return Types;
            },
            getComplexities: function(){
                return Complexities;
            }
        }
	}
})()