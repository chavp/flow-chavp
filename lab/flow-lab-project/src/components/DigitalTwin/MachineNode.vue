
<script lang="ts" setup>
import type { CSSProperties } from 'vue'
import type { NodeProps } from '@vue-flow/core'
import { Handle, Position } from '@vue-flow/core'
import { NodeToolbar } from '@vue-flow/node-toolbar'
import axios from 'axios';

interface Data {
  name: string,
  currentTime: number,
  totalProcessTimeSeconds: number
}

interface AdditionalProps {
  baseUrl: string;
}

const props = defineProps<Pick<NodeProps<Data>, 'id' | 'data'> & AdditionalProps>()

const targetHandleStyle: CSSProperties = { background: '#555' }
const sourceHandleStyleA: CSSProperties = { ...targetHandleStyle }

async function start() {
  await axios
    .put(`${props.baseUrl}/flows/nodes/${props.id}/start`)
    .then(response => {
        
    })
    .catch(error => console.log(error));
}

async function pause() {
  await axios
    .put(`${props.baseUrl}/flows/nodes/${props.id}/pause`)
    .then(response => {
        
    })
    .catch(error => console.log(error));
}

</script>

<template>
  <div class="vue-flow__node-default">
    <NodeToolbar>
      <button v-on:click="start">start</button>
      <button v-on:click="pause">pause</button>
    </NodeToolbar>

    {{ data.name }}
    <div>Usage Time: <strong>{{ data.totalProcessTimeSeconds }}</strong></div>
    <div>Current Time: <strong>{{ data.currentTime }}</strong></div>
  </div>
  <Handle id="cpu" type="source" :position="Position.Right" :style="sourceHandleStyleA" />
</template>
