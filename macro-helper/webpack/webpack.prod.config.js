const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const WebpackCleanupPlugin = require('webpack-cleanup-plugin');
const loaders = require('./webpack.loaders');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const extractNormalizeCSS = new ExtractTextPlugin('normalize.[md5:contenthash:hex:20].css');
const extractCxUI = new ExtractTextPlugin('cx-ui.[md5:contenthash:hex:20].css');
const extractCSS = new ExtractTextPlugin({
    allChunks: true,
    filename: 'portal.[md5:contenthash:hex:20].css'
});

loaders.push({
    test: /\.scss$/,
    exclude: /node_modules/,
    use: extractCSS.extract({
        fallback: 'style-loader',
        use: [
            'css-loader?camelCase&modules&sourceMap&importLoaders=1&localIdentName=[local]___[hash:base64:5]',
            'sass-loader?sourceMap'
        ]
    })
});

loaders.push({
    test: /normalize\.css$/,
    use: extractNormalizeCSS.extract({
        fallback: 'style-loader',
        use: {
            loader: 'css-loader?sourceMap&importLoaders=1',
            options: {
                minimize: false,
                sourceMap: true
            }
        }
    })
});

loaders.push({
    test: /cx-ui\.css$/,
    use: extractCxUI.extract({
        fallback: 'style-loader',
        use: {
            loader: 'css-loader?sourceMap&importLoaders=1',
            options: {
                minimize: false,
                sourceMap: true
            }
        }
    })
});

/**@type {webpack.Configuration} */
const config = {
    entry: [
        'babel-polyfill',
        './src/app.js'
    ],
    output: {
        publicPath: './',
        path: path.resolve('dist'),
        filename: 'bundle-[chunkhash].js'
    },
    resolve: {
        extensions: ['.js', '.jsx', '.webpack.js', '.web.js'],
        modules: ['node_modules']
    },
    module: {
        rules: loaders
    },
    devtool: 'source-map',
    plugins: [
        extractNormalizeCSS,
        extractCxUI,
        extractCSS,
        new HtmlWebpackPlugin({
            template: './src/index.html',
            files: {
                css: ['style.css'],
                js: ['bundle.js'],
            },
            favicon: './src/favicon.ico'
        }),
        new WebpackCleanupPlugin(),
        new webpack.DefinePlugin({
            'process.env.NODE_ENV': JSON.stringify('production')
        })
    ]
};

module.exports = config;