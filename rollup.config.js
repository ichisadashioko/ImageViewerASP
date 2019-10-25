import typescript from 'rollup-plugin-typescript';
import resolve from 'rollup-plugin-node-resolve';
import commonjs from 'rollup-plugin-commonjs';

const extensions = [
    ".js", ".jsx", ".ts", ".tsx"
];

export default {
    input: './ImageViewerASP/Scripts/src/Index.tsx',
    output: [
        {
            name: 'index',
            file: './ImageViewerASP/Scripts/dist/index.esm.js',
            format: 'esm',
        },
        {
            name: 'index',
            file: './ImageViewerASP/Scripts/dist/index.cjs.js',
            format: 'cjs',
        },
    ],
    external: [
        'react',
        'react-dom',
    ],
    plugins: [
        resolve({ extensions }),
        typescript(),
        commonjs({
            namedExports: {
                'deepmerge': ['deepmerge'],
                'react-is': ['ForwardRef'],
            }
        }),
    ]
}