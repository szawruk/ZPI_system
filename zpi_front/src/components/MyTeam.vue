<template>
  <div class="my-team">
    <div class="menu-header">
      <div class="menu-header-option" :class="tasksSelected ? 'selected' : ''" @click="openTasks()">
        Zadania
      </div>
      <div class="menu-header-option" :class="messagesSelected ? 'selected' : ''" @click="openMessages()">
        Wiadomości
      </div>
    </div>
    <template v-if="selectedMenuOption === 1">
      <add-new :method="() =>this.$router.push('/tasks/new')"/>
      <div class="task" v-for="task in tasksList" :key="task.id">
        <div class="task-image">
          <img src="https://www.nicepng.com/png/full/815-8157090_png-file-task-manager-icon-png.png" alt="topic"/>
        </div>
        <div class="task-content">
          <div class="task-content-top">
            <div class="task-number">
              {{ topic.name }}
            </div>
            <div class="task-name">
              {{ topic.name }}
            </div>
            <div class="topic-deadline">
              {{ topic.description }}
            </div>
          </div>
          <div class="task-content-bottom">
            <div class="task-name">
              {{ topic.person }}
            </div>
            <div class="topic-deadline">
              {{ topic.description }}
            </div>
          </div>

        </div>
      </div>
    </template>

  </div>
</template>

<script>
import AddNew from "@/components/controls/AddNew";

export default {
  name: "MyTeam",
  components: {
    AddNew
  },
  computed: {
    tasksSelected() {
      return this.selectedMenuOption === 1
    },
    messagesSelected() {
      return this.selectedMenuOption === 2
    },
    selectedMenuOption() {
      return this.$store.state.myTeam.selectedMenuOptions
    },
    tasksList() {
      return this.$store.state.myTeam.tasksList
    }
  },
  methods: {
    openTasks() {
      this.$store.commit('myTeam/setSelectedMenuOptions', 1)
    },
    openMessages() {
      this.$store.commit('myTeam/setSelectedMenuOptions', 2)
    }

  },
  watch: {
    selectedMenuOption() {

    }
  },
  mounted() {
    this.$store.dispatch('myTeam/getTasks')
  }
}
</script>

<style scoped lang="scss">
.my-team {
  width: 100%;
  margin-top: 30px;

  .menu-header {
    display: flex;
    font-size: 22px;
    background-color: #161b22;
    height: 70px;
    border-radius: 10px;
    margin-bottom: 30px;

    .menu-header-option {
      flex: 1;
      display: flex;
      justify-content: center;
      align-items: center;
      transition: all ease-in-out .2s;
      border-radius: 10px;
      cursor: pointer;

      &.selected {
        background-color: #727272;
      }

      &:hover {
        &:not(.selected) {
          font-size: 24px
        }
      }
    }
  }
}
</style>