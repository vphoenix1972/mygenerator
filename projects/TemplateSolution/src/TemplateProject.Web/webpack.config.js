// ReSharper disable UndeclaredGlobalVariableUsing
'use strict';

const path = require('path');

const webpack = require('webpack');
// ReSharper disable once InconsistentNaming
const NODE_ENV = process.env.NODE_ENV || 'development';
// ReSharper disable once InconsistentNaming
const IsDevelopment = NODE_ENV === 'development';

const CleanWebpackPlugin = require('clean-webpack-plugin');

const UglifyJSPlugin = require('uglifyjs-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

const autoprefixer = require('autoprefixer');

module.exports = {
    performance: {
        hints: false
    },
    context: path.join(__dirname, '/frontend'),
    entry: {
        app: './entryPoint.ts',
        polyfills: './polyfills.ts'
    },
    output: {
        path: __dirname + '/wwwroot/',
        filename: 'dist/[name].bundle.js',
        publicPath: '/',
        libraryTarget: 'var',
        library: '[name]'
    },
    module: {
        rules: [
            {
                test: /\.ts$/, // определяем тип файлов
                use: [
                    {
                        loader: 'awesome-typescript-loader',
                        options: { configFileName: path.resolve(__dirname, 'tsconfig.json') }
                    },
                    'angular2-template-loader'
                ]
            },
            {
                test: /\.html$/,
                loader: 'file-loader?name=templates/[path][name].[hash:6].[ext]'
            },
            {
                test: /\.css$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    {
                        loader: 'css-loader',
                        options: {
                            minimize: !IsDevelopment,
                            sourceMap: true
                        }
                    },
                    {
                        loader: 'postcss-loader',
                        options: {
                            plugins: function () {
                                return [autoprefixer];
                            }
                        }
                    }
                ]
            },
            {
                test: /\.(png|jpg|gif|woff|woff2|ttf|svg|eot)$/,
                loader: 'url-loader?name=assets/[name]-[hash:6].[ext]&limit=4096'
            },
            {
                test: /favicon.ico$/,
                loader: 'file-loader?name=/[name].[ext]'
            }
        ],
        //Optimization. Put independent libs which don't require resolving dependencies.
        noParse: [
            path.join(__dirname, 'node_modules/jquery/dist/jquery.js')            
        ]
    },
    plugins: [
        new webpack.NoEmitOnErrorsPlugin(),
        new CleanWebpackPlugin(
            [
                './wwwroot/dist',
                './wwwroot/assets',
                './wwwroot/templates'
            ]
        ),
        new webpack.DefinePlugin({
            NODE_ENV: JSON.stringify(NODE_ENV)
        }),
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery',
            'window.jQuery': 'jquery'
        }),
        new MiniCssExtractPlugin({
            filename: "dist/[name].css"
        }),
    ],
    resolve: {
        modules: ['node_modules'],
        extensions: ['.ts', '.js'],
        alias: {
            rootDir: path.join(__dirname, '/frontend'),

            // Using minified version for remove console.assert()
            // https://github.com/inexorabletash/polyfill#files
            //'js-polyfills/min': path.join(__dirname, 'node_modules/js-polyfills/polyfill.min.js'),

            // Font awesome 5
            // 'Web Fonts & CSS' version
            //'font-awesome': path.join(__dirname, 'node_modules/@fortawesome/fontawesome-free/css/all.css'),
            // 'SVG & JS' version
            //'font-awesome': path.join(__dirname, 'node_modules/@fortawesome/fontawesome-free/js/all.js'),

            //'angularjs-toaster.css': path.join(__dirname, 'node_modules/angularjs-toaster/toaster.css'),
            //'angularjs-toaster.js': path.join(__dirname, 'node_modules/angularjs-toaster/toaster.js')
        }
    },
    devtool: 'source-map',
    watch: IsDevelopment,
    watchOptions: {
        aggregateTimeout: 300
    }
};

if (!IsDevelopment) {
    module.exports.plugins.push(
        new webpack.optimize.UglifyJsPlugin({
            sourceMap: true
        }));
}

//Use https://www.npmjs.com/package/webpack-bundle-analyzer for analyze bundle size
//Default url http://127.0.0.1:8888/
if (NODE_ENV === 'profiling') {
    var BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;
    module.exports.plugins.push(new BundleAnalyzerPlugin());
}