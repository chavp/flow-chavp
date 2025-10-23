<script setup lang="ts">

import Learning from './components/Learning.vue'
import Basic from './components/Basic/Basic.vue'
import BasicOptionsAPI from './components/Basic/BasicOptionsAPI.vue'
import ConfirmDeleteExample from './components/ConfirmDelete/ConfirmDeleteExample.vue'
import CustomConnectionLine from './components/CustomConnectionLine/CustomConnectionLine.vue'
import CustomNode from './components/CustomNode/CustomNode.vue'
import DnD from './components/DragNDrop/DnD.vue'
import EasyConnect from './components/EasyConnect/EasyConnect.vue'
import EdgeTypesExample from './components/EdgeTypes/EdgeTypesExample.vue'
import Empty from './components/Empty/EmptyExample.vue'
import FloatingEdges from './components/FloatingEdges/FloatingEdges.vue'
import HiddenExample from './components/Hidden/HiddenExample.vue'
import InteractionExample from './components/Interaction/InteractionExample.vue'
import LayoutingExample from './components/Layouting/LayoutingExample.vue'
import MathExample from './components/Math/MathExample.vue'
import MultiFlowsExample from './components/MultiFlows/MultiFlowsExample.vue'
import Nesting from './components/Nesting/Nesting.vue'
import NodeResizerExample from './components/NodeResizer/NodeResizerExample.vue'
import NodeToolbarExample from './components/NodeToolbar/NodeToolbarExample.vue'
import NodeTypeChangeExample from './components/NodeTypeChange/NodeTypeChangeExample.vue'
import Overview from './components/Overview/Overview.vue'
import PiniaExample from './components/Pinia/PiniaExample.vue'
import ProviderExample from './components/Provider/ProviderExample.vue'
import RGBFlow from './components/RGBFlow/RGBFlow.vue'
import SaveRestoreExample from './components/SaveRestore/SaveRestoreExample.vue'
import ScreenshotExample from './components/Screenshot/ScreenshotExample.vue'
import SnapHandleExample from './components/SnapHandle/SnapHandleExample.vue'
import StressExample from './components/Stress/StressExample.vue'
import SwitchExample from './components/Switch/SwitchExample.vue'
import UnidirectionalExample from './components/Unidirectional/UnidirectionalExample.vue'
import UpdatableEdgeExample from './components/UpdatableEdge/UpdatableEdgeExample.vue'
import UpdateNodeExample from './components/UpdateNode/UpdateNodeExample.vue'
import ValidationExample from './components/Validation/ValidationExample.vue'

import Persistance from './components/Persistance/Persistance.vue'
</script>

<template>
  <div class="flow-editor">
    <div class="flow-canvas">
      <Persistance />
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