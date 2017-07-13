module.exports = function (context, req) {
    context.log('JS HTTP function processed a request: ' + req.body.text);

    context.bindings.slackMessage = req.body;
    context.done();
};