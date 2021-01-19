<template>
  <div class="topics">
    <div class="topic" v-for="topic in topicsList" :key="topic.topic">
      <div class="topic-image">
        <img src="https://www.nicepng.com/png/full/517-5178467_png-file-svg-topic-icon-png.png" alt="topic"/>
      </div>
      <div class="topic-content">
        <div class="topic-name">
          {{ topic.topic }}
        </div>
        <div class="topic-description">
          {{ topic.description }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Topics",
  computed: {
    topicsList() {
      return this.$store.state.topics.topicsList
    }
  }
}
</script>

<style scoped lang="scss">
.topics {
  width: 100%;
  margin-top: 50px;

  .topic {
    height: 150px;
    border: solid 2px var(--border-color-1);
    background-color: var(--bg-block-color);
    padding: 20px;
    margin-bottom: 30px;
    display: flex;
    color: white;

    .topic-image {
      flex: 2;
      display: flex;
      justify-content: center;
      align-items: center;
      img{
        height: 80px;
        object-fit: cover;
      filter: invert(96%) sepia(10%) saturate(1236%) hue-rotate(61deg) brightness(99%) contrast(88%);
      }
    }

    .topic-content {
      flex: 14;
      margin-left: 30px;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      padding: 5px;

      .topic-name {
        width: 100%;
        border-bottom: solid 1px var(--border-color-2);
        padding: 10px;
      }

      .topic-description {
        width: 100%;
        padding: 10px;
      }


    }
  }

}
</style>