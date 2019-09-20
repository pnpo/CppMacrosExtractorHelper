const EXCLUDE_FOLDERS = /node_modules/;

/**@type {import('webpack').RuleSetRule[]}*/
const loaders = [
    {
        test: /\.(js|jsx)$/,
        loader: 'babel-loader',
        exclude: EXCLUDE_FOLDERS
    },
    {
        test: /\.svg(\?v=\d+\.\d+\.\d+)?$/,
        loader: 'url-loader?limit=10000&mimetype=image/svg+xml',
        exclude: EXCLUDE_FOLDERS
    },
    {
        test: /\.(ttf|eot|svg|woff(2)?)(\?[a-z0-9]+)?$/,
        loader: 'file-loader'
    },
    {
        test: /\.(jpg|png|svg|ico)$/,
        loader: 'file-loader?name=./images/[hash].[ext]'
    }
];

module.exports = loaders;