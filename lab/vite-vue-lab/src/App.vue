<script setup lang="ts">
import { ref } from 'vue'
import HelloWorld from './components/HelloWorld/HelloWorld.vue'
import BlogPostExample from './components/BlogPost/BlogPostExample.vue'
import TemplateSyntax from './components/TemplateSyntax/TemplateSyntax.vue'
import ComputedProperties from './components/ComputedProperties/ComputedProperties.vue'
import TodoList from './components/TodoList/TodoList.vue'
import VModel from './components/VModel/VModel.vue'
import MouseComponent from './components/MouseComponent/MouseComponent.vue'

const myText = ref('')

</script>

<template>
  <MouseComponent></MouseComponent>
</template>

<style scoped>

</style>
