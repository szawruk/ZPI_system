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
      <add-new :method="() =>this.$router.push('my-team/tasks/new')"/>
      <div class="task" v-for="(task, index) in tasksList" :key="task.id">
        <div class="task-image">
          <img src="https://www.nicepng.com/png/full/815-8157090_png-file-task-manager-icon-png.png" alt="topic"/>
        </div>
        <div class="task-content">
          <div class="task-content-top">
            <div class="task-number">
              <span class="label">Numer:</span> {{ index + 1 }}
            </div>
            <div class="task-name">
              <span class="label">Nazwa:</span>{{ task.name }}
            </div>
            <div class="task-deadline">
              <span class="label">Deadline:</span>{{ task.deadline }}
            </div>
          </div>
          <div class="task-content-bottom">
            <div class="task-person">
              <span class="label">Autor:</span> {{ task.student.name +' ' + task.student.surname }}
            </div>
            <div class="task-description">
              <span class="label">Opis:</span>{{ task.description }}
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
    height: 50px;
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

  .task {
    height: 150px;
    border: solid 2px var(--border-color-1);
    background-color: var(--bg-block-color);
    padding: 20px;
    margin-bottom: 30px;
    display: flex;
    color: white;

    .task-image {
      flex: 2;
      display: flex;
      justify-content: center;
      align-items: center;

      img {
        height: 80px;
        object-fit: cover;
        filter: invert(96%) sepia(10%) saturate(1236%) hue-rotate(61deg) brightness(99%) contrast(88%);
      }
    }

    .task-content {
      flex: 14;
      display: flex;
      flex-direction: column;

      .label{
        color: #929292;
        margin-right: 6px;
      }

      .task-content-top {
        flex: 1;
        display: flex;
        margin-bottom: 10px;

        div{
          display: flex;
          justify-content: center;
          border-bottom: solid 1px var(--border-color-2);
        }

        .task-number{
          width: 50px;
          justify-content: flex-start;
        }
        .task-name{
          flex: 6;
        }
        .task-deadline{
          flex: 6;
        }

      }

      .task-content-bottom {
        flex: 2;
        display: flex;
        align-items: center;

        .task-person{
          flex: 1;
        }
        .task-description{
          flex: 4;
        }
      }
    }


  }
}
</style>