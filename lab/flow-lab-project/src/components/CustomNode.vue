<script setup lang="ts">
import type { NodeProps, useHandle } from '@vue-flow/core'  
import { Position, Handle } from '@vue-flow/core'
import { ref } from 'vue'
import { NodeToolbar } from '@vue-flow/node-toolbar'
import { NodeResizer } from '@vue-flow/node-resizer'

import { CustomData, CustomEvents } from './nodes'

// make sure to include the necessary styles!
import '@vue-flow/node-resizer/dist/style.css'

// props were passed from the slot using `v-bind="customNodeProps"`
const props = defineProps<NodeProps<CustomData, CustomEvents>>()

const listItems = ref(Array.from({ length: 100 }, (_, i) => i))  

console.log(props.data.hello) // 'world'

const inputValue = ref('')
</script>

<template>
  <div class="vue-flow__node-custom">
    <NodeToolbar :is-visible="data.toolbarVisible" :position="data.toolbarPosition">
      <button>Delete</button>
      <button>Duplicate</button>
      <button>Edit</button>
    </NodeToolbar>

    <NodeResizer min-width="100" min-height="30" />
    
    <Handle type="target" :position="Position.Left" />

    <div>{{ props.data.label }}: {{ inputValue }}</div>

    <ul class="nowheel">
      <li v-for="item in listItems" :key="item">Item {{ item }}</li>
    </ul>
    <input class="nodrag" v-model="inputValue" />

    <Handle type="source" :position="Position.Right" />
  </div>
</template>

<style>

    .nowheel{
        height: 200px;
        overflow: auto;
    }
        /* Use a purple theme for our custom node */
    .vue-flow__node-custom {
        background: purple;
        color: white;
        border: 1px solid purple;
        border-radius: 4px;
        box-shadow: 0 0 0 1px purple;
        padding: 8px;
    }
</style>