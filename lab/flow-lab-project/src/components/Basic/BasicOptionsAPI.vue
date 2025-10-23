<script lang="ts">
import { defineComponent } from 'vue'
import type { Elements, FlowEvents, VueFlowStore } from '@vue-flow/core'
import { VueFlow, isNode } from '@vue-flow/core'

import { Background } from '@vue-flow/background'
import { Controls } from '@vue-flow/controls'
import { MiniMap } from '@vue-flow/minimap'
import type { UnwrapNestedRefs } from 'vue'

export default defineComponent({
  name: 'BasicOptionsAPI',
  components: { VueFlow, Background, MiniMap, Controls },
  data() {
    return {
      instance: null as VueFlowStore | null,
      elements: [
        { id: '1', type: 'input', label: 'Node 1', position: { x: 250, y: 5 }, class: 'light' },
        { id: '2', label: 'Node 2', position: { x: 100, y: 100 }, class: 'light' },
        { id: '3', label: 'Node 3', position: { x: 400, y: 100 }, class: 'light' },
        { id: '4', label: 'Node 4', position: { x: 400, y: 200 }, class: 'light' },
        { id: 'e1-2', source: '1', target: '2', animated: true },
        { id: 'e1-3', source: '1', target: '3' },
      ] as Elements,
    }
  },
  methods: {
    logToObject() {
      console.log(this.instance?.toObject())
    },
    resetTransform() {
      this.instance?.setTransform({ x: 0, y: 0, zoom: 1 })
    },
    toggleclass() {
      this.elements.forEach((el) => (el.class = el.class === 'light' ? 'dark' : 'light'))
    },
    updatePos() {
      this.elements.forEach((el) => {
        if (isNode(el)) {
          el.position = {
            x: Math.random() * 400,
            y: Math.random() * 400,
          }
        }
      })
    },
    onNodeDragStop(e: FlowEvents['nodeDragStop']) {
      console.log('drag stop', e)
    },
    onPaneReady(instance: UnwrapNestedRefs<FlowEvents['paneReady']>) {
      instance.fitView()
      this.instance = instance
    },
    onConnect(params: FlowEvents['connect']) {
      this.instance?.addEdges(params)
    },
  },
})
</script>

<template>
    <div class="flow-editor">
        <div class="flow-canvas">
        <VueFlow
            v-model="elements"
            :default-zoom="1.5"
            :min-zoom="0.2"
            :max-zoom="4"
            :zoom-on-scroll="false"
            fit-view-on-init
            @connect="onConnect"
            @pane-ready="onPaneReady"
            @node-drag-stop="onNodeDragStop"
        >
            <Background />
            <MiniMap />
            <Controls />
            <div style="position: absolute; right: 10px; top: 10px; z-index: 4">
                <button style="margin-right: 5px" @click="resetTransform">reset transform</button>
                <button style="margin-right: 5px" @click="updatePos">change pos</button>
                <button style="margin-right: 5px" @click="toggleclass">toggle class</button>
                <button @click="logToObject">toObject</button>
            </div>
        </VueFlow>
    </div>
  </div>
</template>

<style>
/* import the necessary styles for Vue Flow to work */
@import '@vue-flow/core/dist/style.css';

/* import the default theme, this is optional but generally recommended */
@import '@vue-flow/core/dist/theme-default.css';
@import '@vue-flow/minimap/dist/style.css';

/* import default controls styles */
@import '@vue-flow/controls/dist/style.css';

.light{
    color: lightblue;
}
.dark{
    color: darkblue;
}

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
</style>