# 🦁 FoodChain

**FoodChain** is a simple **3D simulation game** where animals interact with each other based on a predefined **food chain**. The goal of this test task is to showcase **clean, scalable, and maintainable code architecture** while ensuring easy extensibility for future animal types.

---

## 🎯 **Task Description**
- **Every (1-2) seconds**, a new **animal** appears and starts moving randomly.
- Animals **collide** using physics.
- If an animal **moves out of bounds**, it should adjust its movement to stay within the screen.

### **Food Chain Rules**
- 🟢 **Prey**: If two prey animals collide, they bounce apart.
- 🔴 **Predators**: If a predator collides with another animal, it eats the prey.
- ⚔️ **Predator vs. Predator**: Younger eats older.

### **Design Patterns Used**
✔️ **Factory Pattern**
✔️ **Strategy Pattern**
✔️ **Object Pooling**

## 🚀 **Technologies Used**
- 🛠 **Zenject**
- 🎨 **UniTask**
- 
