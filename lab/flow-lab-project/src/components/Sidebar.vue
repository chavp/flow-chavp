<script setup lang="ts">
import { useVueFlow } from '@vue-flow/core'

const props = defineProps(['flowId'])
const { nodesSelectionActive, addSelectedNodes, getNodes } = useVueFlow(props.flowId)

const selectAll = () => {
  addSelectedNodes(getNodes.value)
  nodesSelectionActive.value = true
}
</script>

<template>
  <aside>
    <div class="description">
      This is an example of how you can access the internal state outside of the Vue VueFlow component.
    </div>
    <div class="selectall">
      <button @click="selectAll">select all nodes</button>
    </div>
  </aside>
</template>