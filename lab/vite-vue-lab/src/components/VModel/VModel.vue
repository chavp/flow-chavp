<script setup lang="ts">

const [model, modifiers] = defineModel({
  set(value) {
    if (modifiers.capitalize) {
      return value.charAt(0).toUpperCase() + value.slice(1)
    }
    if (modifiers.uppercase) {
      return value.toUpperCase()
    }
    return value
  }
})

</script>

<template>
    <slot name="label"></slot><input type="text" v-model.capitalize="model" />
</template>
