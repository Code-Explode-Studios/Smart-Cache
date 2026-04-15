<img width="1920" height="941" alt="image" src="https://github.com/user-attachments/assets/9bed8a77-6f25-4819-bc61-4811a3560ff4" />

# Smart Domain Reload Manager

A Unity Editor extension designed to eliminate the wait times when entering Play Mode. By dynamically automating Unity's Domain Reload architecture, this tool provides a seamless "Instant Play" environment while ensuring a domain reload is only necessary when making changes to scripts.

## Features

* **Intelligent Domain Automation** : Automatically toggles the **Disable Domain Reload** state by monitoring the compilation pipeline in real time.
* **Unrestricted Editor Workflow** : Enjoy absolute creative freedom. Add GameObjects, construct complex UI hierarchies, swap materials, and refine your scene all while maintaining an instant entry play mode. If you haven't compiled code, you don't wait.
* **Visual Status HUD** : A centered, high visibility status indicator that provides immediate feedback on your cache state, ensuring you know exactly when a reload is pending.
* **Force Manual Override** : Includes a **Force Clear Cache** button for those moments when you want to bypass the logic and ensure a total architectural reset.
* **Zero Latency Iteration** : Drastically reduces developer downtime by bypassing the waiting for domain reloads during design play session iterations.

## Installation

1. Import the `SmartDomainReloadManager.cs` script into your Unity Project.
2. Ensure the script is placed inside an **Editor** folder to function correctly.
3. Access the manager via: **Code Explode Studios Tools > Smart Domain Reload**.

## Customization

The tool is designed for an out of the box experience, but you can refine the following within the script:

* **PrefKeyActive** : Customize the string used for **EditorPrefs** to avoid collisions in large-scale projects.
* **Button Heights** : Adjust the UI scale within the `OnGUI` function to fit your specific display resolution.
* **Status Logic** : Modify the logic within the `UpdateUnitySettings` function to further specialize how reloads are handled in your specific pipeline.
