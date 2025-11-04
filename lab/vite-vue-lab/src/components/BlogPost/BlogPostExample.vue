<script setup lang="ts">
import {ref} from 'vue'
import BlogPost from './BlogPost.vue'
import AlertBox from './AlertBox.vue'

const posts = ref([
  { id: 1, title: 'My journey with Vue' },
  { id: 2, title: 'Blogging with Vue' },
  { id: 3, title: 'Why Vue is so fun' }
])

const postFontSize = ref(1)
function handleEnlargeTextEvent(eventMessage){
  postFontSize.value += 0.1
  console.log(eventMessage)
}

const isShowSlot = ref(false)
</script>

<template>
  <div :style="{ fontSize: postFontSize + 'em' }">
      <BlogPost 
        v-for="post in posts"
        :key="post.id"
        :title="post.title"
        @enlarge-text-event="handleEnlargeTextEvent"
      />
    <hr></hr>
    <input type="checkbox" v-model="isShowSlot">Show slot</input>
    <AlertBox>
      <div :hidden="!isShowSlot">Show in slot</div>
    </AlertBox>
  </div>
</template>

<style scoped>

</style>