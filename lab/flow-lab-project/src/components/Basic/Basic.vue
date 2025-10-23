<script lang="ts" setup>
import { ref } from 'vue'
import type { Edge, Node, ValidConnectionFunc } from '@vue-flow/core'
import { ConnectionMode, Panel, VueFlow, isNode, useVueFlow } from '@vue-flow/core'

import { Background } from '@vue-flow/background'
import { Controls } from '@vue-flow/controls'
import { MiniMap } from '@vue-flow/minimap'

const nodes = ref<Node[]>([
  { id: '1', type: 'input', label: 'Node 1', position: { x: 250, y: 5 }, class: 'light' },
  { id: '2', label: 'Node 2', position: { x: 100, y: 100 }, class: 'light'},
  { id: '3', label: 'Node 3', position: { x: 400, y: 100 }, class: 'light'},
  { id: '4', label: 'Node 4', position: { x: 400, y: 200 }, class: 'light'},
])

const edges = ref<Edge[]>([
  { id: 'e1-2', source: '1', target: '2', animated: true },
  { id: 'e1-3', source: '1', target: '3' },
])

const { onConnect, addEdges, setViewport, toObject, updateNode } = useVueFlow({
  minZoom: 0.2,
  maxZoom: 4,
})

onConnect(addEdges)

function updatePos() {
  return nodes.value.forEach((el) => {
    if (isNode(el)) {
      updateNode(el.id, { position: {x: Math.random() * 400, y: Math.random() * 400 } })
    }
  })
}

function logToObject() {
  return console.log(toObject())
}
function resetViewport() {
  return setViewport({ x: 0, y: 0, zoom: 1 })
}
function toggleclass() {
  return nodes.value.forEach((el) => {
    const newClass = (el.class === 'light' ? 'dark' : 'light')
    updateNode(el.id, { class: newClass })
  })
}

const isValidConnection: ValidConnectionFunc = (...args) => {
  console.log(args)
  return true
}
</script>

<template>
    <div class="flow-editor">
    <div class="flow-canvas">
        <VueFlow
            :nodes="nodes"
            :edges="edges"
            :connection-mode="ConnectionMode.Strict"
            :is-valid-connection="isValidConnection"
            fit-view-on-init
        >
            <Background />
            <MiniMap />
            <Controls />
            <Panel position="top-right" style="display: flex; gap: 5px">
            <button @click="resetViewport">reset viewport</button>
            <button @click="updatePos">change pos</button>
            <button @click="toggleclass">toggle class</button>
            <button @click="logToObject">toObject</button>
            </Panel>
        </VueFlow>
        </div>
    </div>
</template>

<style lang="css">
/* import the necessary styles for Vue Flow to work */
@import '@vue-flow/core/dist/style.css';

/* import the default theme, this is optional but generally recommended */
@import '@vue-flow/core/dist/theme-default.css';
@import '@vue-flow/minimap/dist/style.css';

/* import default controls styles */
@import '@vue-flow/controls/dist/style.css';

.flow-editor {
  width: 100vw;
  height: 100vh;
  position: fixed;
  top: 0;
  left: 0;
}

/* Dark theme support */
@media (prefers-color-scheme: dark) {
  .flow-canvas {
    width: 100%;
    height: 100%;
    background: #0f1419;
    background-image: 
      radial-gradient(circle, #374151 1px, transparent 1px);
    background-size: 20px 20px;
  }
}

.light{
    color: lightblue;
}
.dark{
    color: darkblue;
}
</style>