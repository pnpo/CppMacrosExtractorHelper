const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const loaders = require('./webpack.loaders');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const WebpackCleanupPlugin = require('webpack-cleanup-plugin');
const extractNormalizeCSS = new ExtractTextPlugin('normalize.css');
const extractCxUI = new ExtractTextPlugin('cx-ui.css');
const extractCSS = new ExtractTextPlugin({
    allChunks: true,
    filename: 'portal.css',
});

const PORT = 50006;

loaders.push({
    test: /\.scss$/,
    exclude: /node_modules/,
    use: ['css-hot-loader?sourceMap'].concat(
        extractCSS.extract({
        fallback: 'style-loader',
        use: [
            'css-loader?camelCase&modules&sourceMap&importLoaders=1&localIdentName=[local]___[hash:base64:5]',
            'sass-loader?sourceMap',
        ],
        })
    )
});

loaders.push({
    test: /normalize\.css$/,
    use: ['css-hot-loader?sourceMap'].concat(extractNormalizeCSS.extract({
        fallback: 'style-loader',
        use: {
            loader: 'css-loader?sourceMap&importLoaders=1',
            options: {
                minimize: false,
                sourceMap: true
            }
        }
    }))
});

loaders.push({
    test: /cx-ui\.css$/,
    use: ['css-hot-loader?sourceMap'].concat(extractCxUI.extract({
        fallback: 'style-loader',
        use: {
            loader: 'css-loader?sourceMap&importLoaders=1',
            options: {
                minimize: false,
                sourceMap: true
            }
        }
    }))
});

/**@type {webpack.Configuration & { devServer?: object }} */
const config = {
    entry: {
        app: [
            'react-hot-loader/patch',
            './src/app.js'
        ],
        vendor: [
            'babel-polyfill',
            'react',
            'react-dom'
        ]
    },
    output: {
        publicPath: '/',
        path: path.resolve('build'),
        filename: 'bundle.dev.[name].js'
    },
    resolve: {
        extensions: ['.js', '.jsx', '.webpack.js', '.web.js'],
        modules: ['node_modules']
    },
    module: {
        rules: loaders
    },
    plugins: [
        extractNormalizeCSS,
        extractCxUI,
        extractCSS,
        new HtmlWebpackPlugin({
            template: './src/index.html',
            filename: 'index.html',
            files: {
                css: ['style.css'],
                js: ['bundle.js'],
            },
            favicon: './src/favicon.ico'
        }),
        new WebpackCleanupPlugin(),
        new webpack.DefinePlugin({
            'process.env.NODE_ENV': JSON.stringify('development')
        })
    ],
    // devtool: 'eval', default for webpack 4
    devServer: {
        contentBase: './',
        hot: true,
        inline: true,
        noInfo: true,
        historyApiFallback: true,
        port: PORT,
        /* use it when proxy is needed
        proxy: {
            '/api/**': {
                target: {
                    host: 'localhost',
                    protocol: 'http:',
                    port: 37757
                },
                changeOrigin: true, // change the origin of the url
                pathRewrite: { '^/api': '' } // rewrites url: in this example removes '/api'
            }
        }
        */
    }
};

module.exports = config;